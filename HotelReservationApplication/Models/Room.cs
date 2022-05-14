namespace HotelReservationApplication.Models
{
    public class Room
    {
        public int Id { get; }
        public int Beds { get; }

        public Room(int id, int beds)
        {
            this.Id = id;
            this.Beds = beds;
        }
    }
}
