using AutoMapper;
using CommonShared.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ContractHandlers.ContractHandler;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;

namespace PraktikaApplication.ContractHandlers.ContractCommand
{
    public class ContractAddHandler : IRequestHandler<ContractAddCommand, int>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ContractAddHandler(AppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(ContractAddCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Orders.FirstOrDefaultAsync(x=> x.Id == request.OrderId) == null)
                throw new Exception("Order not found");

            var contract = _mapper.Map<Contract>(request);

            contract.Status = true;

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return contract.Id;
        }
    }
}
