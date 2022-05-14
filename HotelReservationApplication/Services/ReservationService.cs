using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HotelReservationApplication.Models;

namespace HotelReservationApplication.Services
{
    internal class ReservationService : IReservationService
    {
        private readonly List<Room> rooms;

        public ReservationService()
        {
            this.rooms = new List<Room>();

            for (var i = 1; i <= 5; i++)
            {
                var room = new Room(i, i).SetReservation(DateTime.Today.AddDays(-i), DateTime.Today.AddDays(i));
                this.rooms.Add(room);
            }
        }

        public async Task<IEnumerable<Room>> GetUnbookedRooms(int beds, DateTime checkIn, DateTime checkOut)
        {
            await Task.Delay(1);

            return this.rooms.Where(room => room.Beds == beds)
                .Where(room => !room.Reservations.Any(x => Reservation.AreDatesEqual(x.CheckIn, checkIn) || Reservation.AreDatesEqual(x.CheckOut, checkOut)));
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForRoom(int roomId)
        {
            await Task.Delay(1);

            return this.rooms.FirstOrDefault(room => room.Id == roomId)?.Reservations;
        }

        public async Task<Reservation> MakeReservation(int roomId, DateTime checkIn, DateTime checkOut)
        {
            await Task.Delay(1);

            var room = this.rooms.FirstOrDefault(room => room.Id == roomId);

            if (room == null) return null;
            room.SetReservation(checkIn, checkOut);

            return room.Reservations.First();
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            await Task.Delay(1);

            return this.rooms;
        }
    }
}
