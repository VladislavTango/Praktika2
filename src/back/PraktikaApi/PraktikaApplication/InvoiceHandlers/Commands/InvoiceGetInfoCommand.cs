using MediatR;
using PraktikaApplication.InvoiceHandlers.Response;

namespace PraktikaApplication.InvoiceHandlers.Commands
{
    public class InvoiceGetInfoCommand : IRequest<InvoiceGetInfoResponse>
    {
        public int Id { get; set; }
        public int TransportationId { get; set; }
    }
}
