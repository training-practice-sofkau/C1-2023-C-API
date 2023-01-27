namespace example.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public bool IsDeleted { get; set; }
        public string ClientName { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }


    }
}
