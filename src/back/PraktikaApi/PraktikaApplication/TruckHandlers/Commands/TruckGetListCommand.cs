using MediatR;
using PraktikaApplication.TruckHandlers.Response;

namespace PraktikaApplication.TruckHandlers.Commands
{
    public class TruckGetListCommand : IRequest<List<TruckGetListResponse>>
    {
        public int PageLength { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; } = "Id";
    }
}
