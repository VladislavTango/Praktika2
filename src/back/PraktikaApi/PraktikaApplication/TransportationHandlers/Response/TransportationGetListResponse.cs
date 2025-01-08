using PraktikaDomain.Enums;

namespace PraktikaApplication.TransportationHandlers.Response
{
    public class TransportationGetListResponse
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string OrderNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Road { get; set; }
        public TransportationStatus TransportationStatus { get; set; }
        public CargoType CargoType { get; set; }
    }
}
