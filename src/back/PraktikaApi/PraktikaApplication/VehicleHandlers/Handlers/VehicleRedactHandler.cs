using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.VehicleHandlers.Commands;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.VehicleHandlers.Handlers
{
    public class VehicleRedactHandler : IRequestHandler<VehicleRedactCommand>
    {
        private readonly AppDbContext _context;

        public VehicleRedactHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(VehicleRedactCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles
                .Include(x => x.Trailer)
                .Include(x => x.Truck)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (vehicle == null)
                throw new Exception("Vehicle not found");

            if (vehicle.Trailer?.Number != request.TrailerNumber)
            {
                var oldTrailer = vehicle.Trailer;
                var newTrailer = await _context.Trailers
                    .FirstOrDefaultAsync(x => x.Number == request.TrailerNumber);

                if (oldTrailer != null)
                    oldTrailer.VehicleId = null;

                if (newTrailer != null)
                {
                    newTrailer.VehicleId = vehicle.Id;
                    vehicle.TrailerId = newTrailer.Id;
                }
            }

            if (vehicle.Truck?.Number != request.TruckNumber)
            {
                var oldTruck = vehicle.Truck;
                var newTruck = await _context.Trucks
                    .FirstOrDefaultAsync(x => x.Number == request.TruckNumber);

                if (oldTruck != null)
                    oldTruck.VehicleId = null;

                if (newTruck != null)
                {
                    newTruck.VehicleId = vehicle.Id;
                    vehicle.TruckId = newTruck.Id;
                }
            }
            vehicle.Status = request.Status;
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

    }
}
