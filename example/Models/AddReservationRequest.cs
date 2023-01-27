namespace example.Models
{
    public class AddReservationRequest
    {
        public string ClientName { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
