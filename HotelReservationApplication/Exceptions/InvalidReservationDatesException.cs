using System;

namespace HotelReservationApplication.Exceptions
{
    public class InvalidReservationDatesException : Exception
    {
        public InvalidReservationDatesException(string message) : base(message)
        {
        }
    }
}
