using System;
using System.Windows;
using System.Windows.Controls;
using HotelReservationApplication.ViewModels;

namespace HotelReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            this.DataContext = mainWindowViewModel ?? throw new ArgumentNullException(nameof(mainWindowViewModel));
            InitializeComponent();

            this.CheckInDate.BlackoutDates.AddDatesInPast();
            this.CheckOutDate.SelectedDate = DateTime.Today.AddDays(1);
        }

        private void CheckInDate_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            var selectedDate = this.CheckInDate.SelectedDate;
            if (selectedDate != null)
            {
                this.CheckOutDate?.BlackoutDates.Clear();
                if (selectedDate.Value.Ticks >= this.CheckOutDate?.SelectedDate?.Ticks)
                {
                    return;
                }
                this.CheckOutDate?.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, selectedDate.Value));
            }
        }
    }
}
