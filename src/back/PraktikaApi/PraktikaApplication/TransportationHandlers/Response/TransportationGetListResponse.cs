using PraktikaDomain;

namespace PraktikaApplication.TransportationHandlers.Response
{
    public class TransportationGetListResponse
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int OrderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Road { get; set; }
        public TransportationStatus TransportationStatus { get; set; }
    }
}
