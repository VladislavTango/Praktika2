using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.TruckHandlers.Commands;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities.TransportEntities;

namespace PraktikaApplication.TruckHandlers.Handlers
{
    public class TruckAddHandler : IRequestHandler<TruckAddCommand, int>
    {
        private readonly AppDbContext _context;

        public TruckAddHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(TruckAddCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Trucks.FirstOrDefaultAsync(x => x.Number == request.Number) != null)
                throw new Exception("This truck already exist");

            var truck = new Truck()
            {
                Number = request.Number,
                Status = true,
                Mark = request.Mark,
            };
            _context.Trucks.Add(truck);

            await _context.SaveChangesAsync();

            return truck.Id;
        }
    }
}
