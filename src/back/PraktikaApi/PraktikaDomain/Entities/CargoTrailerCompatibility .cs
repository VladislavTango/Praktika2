using PraktikaDomain.Enums;

namespace PraktikaDomain.Entities
{
    public class CargoTrailerCompatibility
    {
        public int Id { get; set; }
        public CargoType CargoType { get; set; }
        public TrailerType TrailerType { get; set; }
    }
}
