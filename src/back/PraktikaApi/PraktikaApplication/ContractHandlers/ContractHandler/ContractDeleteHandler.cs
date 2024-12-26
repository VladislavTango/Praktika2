using CommonShared.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ContractHandlers.ContractHandler;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.ContractHandlers.ContractCommand
{
    public class ContractDeleteHandler : IRequestHandler<ContractDeleteCommand, int>
    {
        private readonly AppDbContext _context;

        public ContractDeleteHandler(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> Handle(ContractDeleteCommand request, CancellationToken cancellationToken)
        {
            var contract = await _context.Contracts.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (contract == null)
                throw new Exception("Contract not found");

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return request.Id;
        }
    }
}
