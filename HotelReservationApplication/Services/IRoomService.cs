using System;
using System.Collections.Generic;
using HotelReservationApplication.Models;

namespace HotelReservationApplication.Services
{
    public interface IRoomService
    {
        IEnumerable<Room> GetRoomsForDates(int beds, DateTime checkInDate, DateTime checkOutDate);
        bool BookRoom(Room room, DateTime checkInDate, DateTime checkOutDate);
    }
}
