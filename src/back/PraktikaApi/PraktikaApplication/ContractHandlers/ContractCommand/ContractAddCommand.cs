using MediatR;

namespace PraktikaApplication.ContractHandlers.ContractHandler
{
    public class ContractAddCommand : IRequest<int>
    {
        public int OrderId { get; set; }
        public string ContractTerms { get; set; }
    }
}
