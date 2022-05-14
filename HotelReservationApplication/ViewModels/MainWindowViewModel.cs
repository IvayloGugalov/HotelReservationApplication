using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HotelReservationApplication.Command;
using HotelReservationApplication.Models;
using HotelReservationApplication.Services;
using HotelReservationApplication.ViewModels.Base;

namespace HotelReservationApplication.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IReservationService reservationService;

        public DateTime CheckInDate
        {
            get => this.checkIn;
            set => this.SetValue(ref this.checkIn, value);
        }
        private DateTime checkIn = DateTime.Now;

        public DateTime CheckOutDate
        {
            get => this.checkOut;
            set => this.SetValue(ref this.checkOut, value);
        }
        private DateTime checkOut = DateTime.Now;

        public ObservableCollection<Room> Rooms { get; set; } = new();

        public ICommand SearchForRoomsCommand { get; }
        public ICommand MakeReservationCommand { get; }

        public MainWindowViewModel(IReservationService reservationService)
        {
            this.reservationService = reservationService;

            this.SearchForRoomsCommand = new RelayCommand(this.SearchForRoomsAction);
            this.MakeReservationCommand = new RelayCommand(this.MakeReservationAction);

            this.SetRooms();
        }

        private async void SetRooms()
        {
            try
            {
                var rooms = await Task.Run(() => this.reservationService.GetRooms());

                foreach (var room in rooms)
                {
                    this.Rooms.Add(room);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void MakeReservationAction(object obj)
        {
            throw new NotImplementedException();
        }

        private void SearchForRoomsAction(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
