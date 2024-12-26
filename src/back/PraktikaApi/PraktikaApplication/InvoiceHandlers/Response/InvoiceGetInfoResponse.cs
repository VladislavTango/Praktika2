using PraktikaApplication.OrderHandlers.Response;
using PraktikaApplication.TransportationHandlers.Response;

namespace PraktikaApplication.InvoiceHandlers.Response
{
    public class InvoiceGetInfoResponse
    {
        public int InvoiceId { get; set; }
        public OrderGetListResponse Order {  get; set; }
        public TransportationGetListResponse Transportation { get; set; }
    }
}
