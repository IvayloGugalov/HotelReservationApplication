using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelReservationApplication.Models;

namespace HotelReservationApplication.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationsForDate(DateTime checkIn, DateTime checkOut);
        Task<Reservation> MakeReservation(int roomId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<Room>> GetRooms();
    }
}
