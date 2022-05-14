using System;

using HotelReservationApplication.Exceptions;

namespace HotelReservationApplication.Models
{
    public class Reservation
    {
        public int Id { get; }
        public int RoomId { get; }
        public DateTime CheckIn { get; private set; }
        public DateTime CheckOut { get; private set; }

        public Reservation(int id, int roomId, DateTime checkIn, DateTime checkOut)
        {
            if (checkIn.Year > checkOut.Year
                || checkIn.Month > checkOut.Month)
            {
                throw new InvalidReservationDatesException("Check out date is invalid");
            }
            if (checkIn.Date >= checkOut.Date
                     && (checkIn.Year <= checkOut.Year && checkIn.Month <= checkOut.Month))
            {
                throw new InvalidReservationDatesException("Check out date is invalid");
            }

            this.Id = id;
            this.RoomId = roomId;
            this.CheckIn = checkIn;
            this.CheckOut = checkOut;
        }

        public static bool AreDatesEqual(DateTime left, DateTime right)
        {
            return left.Date == right.Date
                   && left.Month == right.Month
                   && left.Year == right.Year;
        }
    }
}
