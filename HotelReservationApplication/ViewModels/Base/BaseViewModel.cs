using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HotelReservationApplication.ViewModels.Base
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private bool disposed;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetValue<TValue>(ref TValue target, TValue value, Action callback = null,
            [CallerMemberName] string propertyName = null)
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            if (Equals(target, value))
            {
                return;
            }

            target = value;

            this.OnPropertyChanged(propertyName);

            callback?.Invoke();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                if (propertyName == null)
                {
                    throw new ArgumentNullException(nameof(propertyName));
                }

                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || this.disposed)
            {
                return;
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
