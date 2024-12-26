namespace PraktikaDomain.Entities
{
    public class Invoices : BaseEntity
    {
        public int TransportationId { get; set; }
        public Transportation Transportation { get; set; }
    }
}
