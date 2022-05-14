using System;
using System.Collections.Generic;

namespace HotelReservationApplication.Models
{
    public class Room
    {
        public int Id { get; }
        public int Beds { get; }

        public IReadOnlyCollection<Reservation> Reservations => this.reservations.AsReadOnly();
        private readonly List<Reservation> reservations = new();

        public Room(int id, int beds)
        {
            this.Id = id;
            this.Beds = beds;
        }

        public Room SetReservation(DateTime checkIn, DateTime checkOut)
        {
            var reservation = new Reservation(Math.Abs(Guid.NewGuid().GetHashCode()), this.Id, checkIn, checkOut);

            this.reservations.Add(reservation);
            return this;
        }
    }
}
