using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.OrderHandlers.OrderCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;
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

            var order = _mapper.Map<Order>(request);
            order.ClientId = Claims.userId;
            order.CreatedDate = DateTime.UtcNow;
            order.Status = true;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }
    }
}
