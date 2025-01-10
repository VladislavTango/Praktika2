using AutoMapper;
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.TruckHandlers.Commands;
using PraktikaApplication.TruckHandlers.Response;
using PraktikaDataPersistance.ApplicationContext;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace PraktikaApplication.TruckHandlers.Handlers
{
    public class TruckGetListHandler : IRequestHandler<TruckGetListCommand, List<TruckGetListResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TruckGetListHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TruckGetListResponse>> Handle(TruckGetListCommand request, CancellationToken cancellationToken)
        {
            var gridifyQuery = new GridifyQuery
            {
                OrderBy = request.SortBy,
            };

            var trucks = await _context.Trucks.Where(x=> x.Vehicle==null)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<TruckGetListResponse>>(trucks);
        }
    }
}
