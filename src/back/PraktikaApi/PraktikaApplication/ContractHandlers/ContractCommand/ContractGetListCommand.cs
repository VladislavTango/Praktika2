using MediatR;
using PraktikaApplication.ContractHandlers.ContractResponse;
using PraktikaDomain.Entities;

namespace PraktikaApplication.ContractHandlers.ContractHandler
{
    public class ContractGetListCommand : IRequest<List<ContractGetListResponse>>
    {
        public int PageLength { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; } = "Id";
    }
}
