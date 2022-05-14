using System;
using HotelReservationApplication.Models.Exceptions;

namespace HotelReservationApplication.Models;

public class BookingDate
{
    public DateTime CheckIn { get; }
    public DateTime CheckOut { get; }

    public BookingDate(DateTime checkIn, DateTime checkOut)
    {
        if (checkIn.Year > checkOut.Year
            || checkIn.Month > checkOut.Month
            || checkIn.Date >= checkOut.Date)
        {
            throw new CheckOutDateIsInvalidException("Check Out date comes before the Check In date");
        }

        this.CheckIn = checkIn;
        this.CheckOut = checkOut;
    }

    public bool AreBookingsEqual(DateTime checkInDate, DateTime checkOutDate)
    {
        return this.CheckIn.Ticks == checkInDate.Ticks && this.CheckOut.Ticks == checkOutDate.Ticks;
    }
}