using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.TransportationHandlers.TransportationCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;
using PraktikaDomain.Enums;

namespace PraktikaApplication.TransportationHandlers.TransportationHandler
{
    public class TransportationAddHandler : IRequestHandler<TransportationAddCommand, int>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TransportationAddHandler(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(TransportationAddCommand request, CancellationToken cancellationToken)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderNumber == request.OrderNumber);
            if (order == null)
                throw new Exception("Order not found");

            var Vehicle = await _context.Vehicles
                .Include(x => x.Truck)
                .Include(x => x.Trailer)
                .FirstOrDefaultAsync(x => x.Id == request.VehicleId);

            if(Vehicle == null)
                throw new Exception("Vehicle not found");

             var isCompatible = await _context.cargoTrailerCompatibilities
            .AnyAsync(x => x.CargoType == request.CargoType && x.TrailerType == Vehicle.Trailer.TrailerType);

            if (isCompatible == false)
                throw new Exception("The types do not match");

            var transportation = _mapper.Map<Transportation>(request);

            transportation.Status = true;
            transportation.TransportationStatus = TransportationStatus.NEW;
            transportation.OrderId = order.Id;

            _context.Transportations.Add(transportation);
            _context.SaveChanges();

            return transportation.Id;
        }
    }
}
