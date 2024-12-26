namespace PraktikaDomain.Entities
{
    public class Transportation : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Road {  get; set; }
        public TransportationStatus TransportationStatus { get; set; }
    }
}
