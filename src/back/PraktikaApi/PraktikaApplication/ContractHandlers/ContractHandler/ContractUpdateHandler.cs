using AutoMapper;
using CommonShared.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ContractHandlers.ContractHandler;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;

namespace PraktikaApplication.ContractHandlers.ContractCommand
{
    public class ContractUpdateHandler : IRequestHandler<ContractUpdateCommand>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContractUpdateHandler(AppDbContext context , IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(ContractUpdateCommand request, CancellationToken cancellationToken)
        {
            var contract = await _context.Contracts.FirstOrDefaultAsync(x => x.Id == request.Id);

            if(contract==null)
                throw new Exception("Contract not found");


            _mapper.Map(request, contract);

            contract.Status = request.Status;

            await _context.SaveChangesAsync();
        }
    }
}
