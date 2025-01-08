using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.VehicleHandlers.Commands;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities.TransportEntities;

namespace PraktikaApplication.VehicleHandlers.Handlers
{
    public class GetVehiclesByCargoHandler : IRequestHandler<GetVehiclesByCargoCommand, List<int>>
    {
        private readonly AppDbContext _context;

        public GetVehiclesByCargoHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> Handle(GetVehiclesByCargoCommand request, CancellationToken cancellationToken)
        {

            var vehiclesId = await _context.Vehicles
            .Where(a => a.Status == true &&
            _context.cargoTrailerCompatibilities
            .Any(b =>
                b.CargoType == request.CargoType &&
                b.TrailerType == a.Trailer.TrailerType))
            .Select(c => c.Id)
            .ToListAsync();

            return vehiclesId;
        }
    }
}
