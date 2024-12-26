using AutoMapper;
using Gridify;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.TransportationHandlers.Response;
using PraktikaApplication.TransportationHandlers.TransportationCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Interfaces;

namespace PraktikaApplication.TransportationHandlers.TransportationHandler
{
    public class TransportationGetListHandler : IRequestHandler<TransportationGetListCommand, List<TransportationGetListResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokentService _jwtTokentService;

        public TransportationGetListHandler(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IJwtTokentService jwtTokentService)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokentService = jwtTokentService;
        }

        public async Task<List<TransportationGetListResponse>> Handle(TransportationGetListCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];

            var Claims = _jwtTokentService.TokenClaimCatcher(authorizationHeader);
            var gridifyQuery = new GridifyQuery()
            {
                OrderBy = request.SortBy,
            };

            var Transportations = await _context.Transportations
            .Where(c => c.Order.ClientId == Claims.userId)
            .Include(c => c.Order)
            .ApplyFiltering(gridifyQuery)
            .ApplyOrdering(gridifyQuery)
            .Skip((request.PageNumber - 1) * request.PageLength)
            .Take(request.PageLength)
            .ToListAsync(cancellationToken);

            return _mapper.Map<List<TransportationGetListResponse>>(Transportations);

        }
    }
}
