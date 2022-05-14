using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using HotelReservationApplication.Services;
using HotelReservationApplication.ViewModels;

namespace HotelReservation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            var viewModel = new MainWindowViewModel(new ReservationService());
            var view = new MainWindow(viewModel);

            view.Show();
        }

        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            this.ShowUnhandledError(e.Exception);
        }

        private void TaskSchedulerOnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            this.ShowUnhandledError(e.Exception);
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.ShowUnhandledError(e.ExceptionObject as Exception);
        }

        private void ShowUnhandledError(Exception exception)
        {
            MessageBox.Show(
                messageBoxText: exception.Message,
                caption: nameof(Exception),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            //Application.Current.Shutdown();
        }
    }
}
