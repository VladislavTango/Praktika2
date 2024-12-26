using MediatR;
using PraktikaDomain.Entities;

namespace PraktikaApplication.ContractHandlers.ContractCommand
{
    public class ContractGetByIdCommand : IRequest<Contract>
    {
        public int Id;
    }
}
