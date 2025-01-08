using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.OrderHandlers.OrderCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;
using PraktikaDomain.Enums;
using PraktikaDomain.Interfaces;

namespace PraktikaApplication.OrderHandlers.OrderHandler
{
    public class OrderAddHandler : IRequestHandler<OrderAddCommand, int>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokentService _jwtTokentService;

        public OrderAddHandler(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IJwtTokentService jwtTokentService)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokentService = jwtTokentService;
        }

        public async Task<int> Handle(OrderAddCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var authorizationHeader =  httpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];

            var Claims = _jwtTokentService.TokenClaimCatcher(authorizationHeader);

            if (await _context.Clients.FirstOrDefaultAsync(x => x.Id == Claims.userId) == null)
                throw new Exception("Client not found");

            var contract = await _context.Contracts
            .FirstOrDefaultAsync(c => c.Id == request.ContractId);

            if (contract == null || contract.Status == false)
            {
                throw new Exception("Contract not found or closed");
            }

            if (request.CreatedDate < contract.ContractDate ||
            (contract.ExpirationDate != null && request.CreatedDate > contract.ExpirationDate))
            {
                throw new Exception("Invalid date");
            }

            if(contract.ContractType == ContractType.ONCE && 
                (await _context.Orders
                .Where(o => o.ContractList
                .Any())
                .AnyAsync(o => o.ContractList
                .Any(c => c.ContractType == ContractType.ONCE && c.Id == contract.Id))))
            {
                throw new Exception("you can't create order with this contract");
            }

            var order = _mapper.Map<Order>(request);
            order.ClientId = Claims.userId;
            order.CreatedDate = DateTime.UtcNow;
            order.Status = true;
            order.OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 6)}";
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();
            contract.OrderId = order.Id;
            await _context.SaveChangesAsync();


            return order.Id;
        }
    }
}
