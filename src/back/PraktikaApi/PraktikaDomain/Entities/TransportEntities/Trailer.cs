using PraktikaDomain.Enums;

namespace PraktikaDomain.Entities.TransportEntities
{
    public class Trailer : BaseTransportEntity
    {
        public int? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public TrailerType TrailerType { get; set; }
    }
}
