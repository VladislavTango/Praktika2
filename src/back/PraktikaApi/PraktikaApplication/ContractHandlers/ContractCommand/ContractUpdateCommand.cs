using MediatR;

namespace PraktikaApplication.ContractHandlers.ContractHandler
{
    public class ContractUpdateCommand : IRequest
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int OrderId { get; set; }
        public string ContractTerms { get; set; }
    }
}
