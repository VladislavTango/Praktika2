using Gridify;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ClientHandlers.ClientCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;
using PraktikaDomain.Interfaces;

namespace PraktikaApplication.ClientHandlers.ClientHandler
{
    public class ClientGetListHandler : IRequestHandler<ClientGetListCommand, List<Client>>
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokentService _jwtTokentService;

        public ClientGetListHandler(AppDbContext context , IHttpContextAccessor httpContextAccessor , IJwtTokentService jwtTokentService) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokentService = jwtTokentService;
        }

        public async Task<List<Client>> Handle(ClientGetListCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];

            var a = _jwtTokentService.TokenClaimCatcher(authorizationHeader);

            var b = a;
            var gridifyQuery = new GridifyQuery()
            {
                OrderBy = request.SortBy,
            };

            var query = _context.Clients
                .ApplyFiltering(gridifyQuery)
                .ApplyOrdering(gridifyQuery);

            return await query.Skip((request.PageNumber - 1) * request.PageLength).Take(request.PageLength).ToListAsync();
        }
    }
}
