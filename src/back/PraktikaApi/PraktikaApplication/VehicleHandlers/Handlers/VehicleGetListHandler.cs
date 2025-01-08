using AutoMapper;
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.TrailerHandlers.Response;
using PraktikaApplication.TruckHandlers.Response;
using PraktikaApplication.VehicleHandlers.Commands;
using PraktikaApplication.VehicleHandlers.Response;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities.TransportEntities;

namespace PraktikaApplication.VehicleHandlers.Handlers
{
    public class VehicleGetListHandler : IRequestHandler<VehicleGetListCommand, List<VehicleGetListResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VehicleGetListHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<VehicleGetListResponse>> Handle(VehicleGetListCommand request, CancellationToken cancellationToken)
        {
            var gridifyQuery = new GridifyQuery
            {
                OrderBy = request.SortBy,
            };

            var vehicles = await _context.Vehicles
                .Include(v => v.Truck) 
                .Include(v => v.Trailer)
                .ApplyFiltering(gridifyQuery)
                .ApplyOrdering(gridifyQuery)
                .Skip((request.PageNumber - 1) * request.PageLength)
                .Take(request.PageLength)
                .ToListAsync(cancellationToken);


            var response = _mapper.Map<List<VehicleGetListResponse>>(vehicles);

            return response;
        }
    }
}
