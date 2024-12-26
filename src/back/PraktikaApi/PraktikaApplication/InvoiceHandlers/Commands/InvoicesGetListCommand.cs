using MediatR;
using PraktikaApplication.InovicesHandlers.Response;

namespace PraktikaApplication.InvoicesHandlers.Commands
{
    public class InvoicesGetListCommand : IRequest<List<InvoicesGetListResponse>>
    {
        public int PageLength { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; } = "Id";
    }
}
