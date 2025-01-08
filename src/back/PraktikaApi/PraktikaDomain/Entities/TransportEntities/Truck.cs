namespace PraktikaDomain.Entities.TransportEntities
{
    public class Truck : BaseTransportEntity
    {
        public string Mark { get; set; }
        public Vehicle Vehicle { get; set; }
        public int? VehicleId { get; set; }
    }
}
