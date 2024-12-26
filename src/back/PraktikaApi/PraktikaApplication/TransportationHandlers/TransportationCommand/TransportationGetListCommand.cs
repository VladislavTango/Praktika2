using MediatR;
using PraktikaApplication.TransportationHandlers.Response;
using PraktikaDomain.Entities;

namespace PraktikaApplication.TransportationHandlers.TransportationCommand
{
    public class TransportationGetListCommand : IRequest<List<TransportationGetListResponse>>
    {
        public int PageLength { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; } = "Id";
    }
}
