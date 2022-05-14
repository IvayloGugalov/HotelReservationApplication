using System;
using System.Collections.Generic;
using System.Linq;
using HotelReservationApplication.Models;

namespace HotelReservationApplication.Services
{
    public class RoomService : IRoomService
    {
        private readonly List<Room> rooms = new();

        private readonly DateTime currentDate = new DateTime(2022, 05, 09);

        public RoomService()
        {
            this.rooms.Add(new Room(1).AddBooking(currentDate.AddDays(5), currentDate.AddDays(10)));
            this.rooms.Add(new Room(2).AddBooking(currentDate.AddDays(8), currentDate.AddDays(19)));
            this.rooms.Add(new Room(3).AddBooking(currentDate.AddDays(10), currentDate.AddDays(15)));
            this.rooms.Add(new Room(4).AddBooking(currentDate.AddDays(-1), currentDate.AddDays(1)));
        }

        public IEnumerable<Room> GetRoomsForDates(int beds, DateTime checkInDate, DateTime checkOutDate)
        {
            var rooms = this.rooms.Where(r => r.Beds == beds && r.Bookings.Any(x => x.AreBookingsEqual(checkInDate, checkOutDate)));

            return rooms;
        }

        public bool BookRoom(Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            var roomToBook = this.rooms.FirstOrDefault(x => x.Beds == room.Beds)?.AddBooking(checkInDate, checkOutDate);

            return roomToBook != null;
        }
    }
}
