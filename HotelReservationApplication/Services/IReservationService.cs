using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelReservationApplication.Models;

namespace HotelReservationApplication.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Room>> GetUnbookedRooms(int beds, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<Reservation>> GetReservationsForRoom(int roomId);
        Task<Reservation> MakeReservation(int roomId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<Room>> GetRooms();
    }
}
