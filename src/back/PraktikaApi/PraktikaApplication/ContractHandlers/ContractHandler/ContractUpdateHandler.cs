using AutoMapper;
using CommonShared.Domains;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ContractHandlers.ContractHandler;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Interfaces;

namespace PraktikaApplication.ContractHandlers.ContractCommand
{
    public class ContractUpdateHandler : IRequestHandler<ContractUpdateCommand>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokentService _jwtTokentService;

        public ContractUpdateHandler(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IJwtTokentService jwtTokentService)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokentService = jwtTokentService;
        }

        public async Task Handle(ContractUpdateCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];

            var Claims = _jwtTokentService.TokenClaimCatcher(authorizationHeader);

            if (!(await _context.Clients.FirstOrDefaultAsync(x => x.Id == Claims.userId)).Status)
                throw new Exception("Incorrect client status");

            var contract = await _context.Contracts.FirstOrDefaultAsync(x => x.Id == request.Id);

            if(contract==null)
                throw new Exception("Contract not found");


            _mapper.Map(request, contract);

            contract.Status = request.Status;

            await _context.SaveChangesAsync();
        }
    }
}
