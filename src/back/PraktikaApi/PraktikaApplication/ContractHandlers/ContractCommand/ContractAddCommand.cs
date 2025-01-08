using MediatR;
using PraktikaDomain.Enums;

namespace PraktikaApplication.ContractHandlers.ContractHandler
{
    public class ContractAddCommand : IRequest<int>
    {
        public string ContractTerms { get; set; }
        public ContractType ContractType { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
