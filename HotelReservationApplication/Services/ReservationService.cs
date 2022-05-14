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
        private readonly HashSet<Reservation> reservations;

        public ReservationService()
        {
            this.rooms = new List<Room>();

            for (var i = 1; i <= 5; i++)
            {
                this.rooms.Add(new Room(i, i));
            }

            this.reservations = new HashSet<Reservation>();

            var rooms = this.rooms.ToArray();
            for (var i = 1; i <= 5; i++)
            {
                this.reservations.Add(new Reservation(i, rooms[i].Id, DateTime.Today.AddDays(i - 2), DateTime.Today.AddDays(i + 2)));
            }
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForDate(DateTime checkIn, DateTime checkOut)
        {
            await Task.Delay(1);

            return this.reservations.Where(x => Reservation.AreDatesEqual(x.CheckIn, checkIn) || Reservation.AreDatesEqual(x.CheckOut, checkOut));
        }

        public async Task<Reservation> MakeReservation(int roomId, DateTime checkIn, DateTime checkOut)
        {
            await Task.Delay(1);

            var id = Math.Abs(Guid.NewGuid().GetHashCode());
            var reservation = new Reservation(id, roomId, checkIn, checkOut);

            this.reservations.Add(reservation);

            return this.reservations.Add(reservation)
                ? reservation
                : null;
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            await Task.Delay(1);

            return this.rooms;
        }
    }
}
