namespace example.Models
{
    public class UpdateReservationRequest
    {
        public string ClientName { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
