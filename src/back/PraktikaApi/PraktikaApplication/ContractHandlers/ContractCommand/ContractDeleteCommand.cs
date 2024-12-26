using MediatR;

namespace PraktikaApplication.ContractHandlers.ContractHandler
{
    public class ContractDeleteCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
