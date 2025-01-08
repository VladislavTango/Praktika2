using MediatR;
using PraktikaApplication.TrailerHandlers.Response;

namespace PraktikaApplication.TrailerHandlers.Commands
{
    public class TrailerGetListCommand : IRequest<List<TrailerGetListResponse>>
    {
        public int PageLength { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; } = "Id";
    }
}
