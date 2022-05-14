using System;

namespace HotelReservationApplication.Models.Exceptions
{
    public class CheckOutDateIsInvalidException : Exception
    {
        public CheckOutDateIsInvalidException(string message) : base(message)
        {
            
        }
    }
}
