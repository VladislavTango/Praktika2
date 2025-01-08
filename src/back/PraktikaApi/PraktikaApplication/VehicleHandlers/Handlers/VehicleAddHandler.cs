using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.VehicleHandlers.Commands;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities.TransportEntities;

namespace PraktikaApplication.VehicleHandlers.Handlers
{
    public class VehicleAddHandler : IRequestHandler<VehicleAddCommand, int>
    {
        private readonly AppDbContext _context;

        public VehicleAddHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(VehicleAddCommand request, CancellationToken cancellationToken)
        {
            var Truck = await _context.Trucks.Include(x => x.Vehicle)
                .FirstOrDefaultAsync(x => x.Number == request.TruckNumber && x.Status!=false);

            if (Truck == null)
                throw new Exception("Truck not found or not active");
            if(Truck.Vehicle != null)
                throw new Exception("this truck is busy");

            var Trailer = await _context.Trailers.Include(x => x.Vehicle)
                .FirstOrDefaultAsync(x => x.Number == request.TrailerNumber && x.Status != false);

            if(Trailer == null)
                throw new Exception("Trailer not found or not active");
            if (Trailer.Vehicle != null)
                throw new Exception("this trailer is busy");

            var vehicle = new Vehicle()
            {
                TruckId = Truck.Id,
                TrailerId = Trailer.Id,
                Truck = Truck,
                Trailer = Trailer,
                Status = true
            };
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle.Id;
        }
    }
}
