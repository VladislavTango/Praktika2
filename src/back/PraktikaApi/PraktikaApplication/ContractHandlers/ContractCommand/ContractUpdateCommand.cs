using MediatR;
using PraktikaDomain.Enums;

namespace PraktikaApplication.ContractHandlers.ContractHandler
{
    public class ContractUpdateCommand : IRequest
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string ContractTerms { get; set; }
        public ContractType ContractType { get; set; }

    }
}
