using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ContractHandlers.ContractHandler;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;
using PraktikaDomain.Interfaces;

namespace PraktikaApplication.ContractHandlers.ContractCommand
{
    public class ContractAddHandler : IRequestHandler<ContractAddCommand, int>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokentService _jwtTokentService;

        public ContractAddHandler(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IJwtTokentService jwtTokentService)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokentService = jwtTokentService;
        }

        public async Task<int> Handle(ContractAddCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];

            var Claims = _jwtTokentService.TokenClaimCatcher(authorizationHeader);

            if (!(await _context.Clients.FirstOrDefaultAsync(x => x.Id == Claims.userId)).Status)
                throw new Exception("Incorrect client status");

            if(request.ExpirationDate<request.ContractDate)
                throw new Exception("Contract unreal");


            var contract = _mapper.Map<Contract>(request);

            contract.Status = true;

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return contract.Id;
        }
    }
}
