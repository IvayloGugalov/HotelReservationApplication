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
            set
            {
                if (this.CheckInDate.Ticks >= value.Ticks)
                {
                    this.SetValue(ref this.checkOut, this.CheckInDate.AddDays(1));
                    return;
                }
                this.SetValue(ref this.checkOut, value);
            }
        }
        private DateTime checkOut = DateTime.Now;

        public Room SelectedRoom
        {
            get => this.selectedRoom;
            set => this.SetValue(ref this.selectedRoom, value);
        }
        private Room selectedRoom;

        public Room SelectedRoomForBooking
        {
            get => this.selectedRoomForBooking;
            set => this.SetValue(ref this.selectedRoomForBooking, value);
        }
        private Room selectedRoomForBooking;

        public ObservableCollection<Room> Rooms { get; set; } = new();
        public ObservableCollection<Room> RoomsForBooking { get; set; }

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

        private async void MakeReservationAction(object obj)
        {
            if (this.SelectedRoomForBooking == null)
            {
                this.SetMessage("Please select a room to book");
                return;
            }
            var reservation = await Task.Run(() => this.reservationService.MakeReservation(this.SelectedRoomForBooking.Id, this.CheckInDate, this.CheckOutDate));

            if (reservation == null)
            {
                this.SetMessage("Could not book you room. Try again!");
            }
        }

        private async void SearchForRoomsAction(object obj)
        {
            this.RoomsForBooking = new ObservableCollection<Room>();
            if (this.SelectedRoom == null)
            {
                this.SetMessage("Please select the number of rooms");
                return;
            }

            var rooms = await Task.Run(() => this.reservationService.GetUnbookedRooms(this.SelectedRoom.Beds, this.CheckInDate, this.CheckOutDate));

            var arrayOfRooms = rooms as Room[] ?? rooms.ToArray();
            if (!arrayOfRooms.Any())
            {
                this.SetMessage($"No rooms with {this.SelectedRoom.Beds} beds available for {this.CheckInDate.ToShortDateString()} - {this.CheckOutDate.ToShortDateString()}");
                this.OnPropertyChanged(nameof(this.RoomsForBooking));
            }

            foreach (var room in arrayOfRooms)
            {
                this.RoomsForBooking.Add(room);
            }
            this.OnPropertyChanged(nameof(this.RoomsForBooking));
        }

        private void SetMessage(string message)
        {
            this.Message = message;
            this.OnPropertyChanged(nameof(this.Message));
        }

        public string Message{ get; private set; }
    }
}
