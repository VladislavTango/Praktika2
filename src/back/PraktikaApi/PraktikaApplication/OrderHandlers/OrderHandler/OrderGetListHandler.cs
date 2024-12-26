using AutoMapper;
using Gridify;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.OrderHandlers.OrderCommand;
using PraktikaApplication.OrderHandlers.Response;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Interfaces;

namespace PraktikaApplication.OrderHandlers.OrderHandler
{
    public class OrderGetListHandler : IRequestHandler<OrderGetListCommand, List<OrderGetListResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokentService _jwtTokentService;

        public OrderGetListHandler(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IJwtTokentService jwtTokentService)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokentService = jwtTokentService;
        }

        public async Task<List<OrderGetListResponse>> Handle(OrderGetListCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];

            var Claims = _jwtTokentService.TokenClaimCatcher(authorizationHeader);

            var gridifyQuery = new GridifyQuery
            {
                OrderBy = request.SortBy,
            };

            var orders = await _context.Orders
                .Where(o => o.ClientId == Claims.userId)
                .ApplyFiltering(gridifyQuery)
                .ApplyOrdering(gridifyQuery)
                .Skip((request.PageNumber - 1) * request.PageLength)
                .Take(request.PageLength)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<OrderGetListResponse>>(orders);
        }
    }
}
