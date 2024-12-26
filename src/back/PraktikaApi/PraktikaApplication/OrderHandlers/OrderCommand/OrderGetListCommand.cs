using MediatR;
using PraktikaApplication.OrderHandlers.Response;
using PraktikaDomain.Entities;

namespace PraktikaApplication.OrderHandlers.OrderCommand
{
    public class OrderGetListCommand : IRequest<List<OrderGetListResponse>>
    {
        public int PageLength { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; } = "Id";
    }
}
