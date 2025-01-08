namespace PraktikaDomain.Entities.TransportEntities
{
    public class Vehicle : BaseEntity
    {
        public int? TruckId { get; set; }
        public Truck? Truck { get; set; }
        public int? TrailerId { get; set; }
        public Trailer? Trailer { get; set; }
    }
}
