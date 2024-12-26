using CommonShared.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ContractHandlers.ContractCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;

namespace PraktikaApplication.ContractHandlers.ContractHandler
{
    public class ContractGetByIdHandler : IRequestHandler<ContractGetByIdCommand, Contract>
    {
        private readonly AppDbContext _context;

        public ContractGetByIdHandler(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<Contract> Handle(ContractGetByIdCommand request, CancellationToken cancellationToken)
        {
            return await _context.Contracts.FirstOrDefaultAsync(x => x.Id == request.Id) ??
                throw new Exception("Contract not found");

        }
    }
}
