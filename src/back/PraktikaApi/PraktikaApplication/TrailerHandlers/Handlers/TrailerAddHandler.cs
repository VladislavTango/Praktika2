using MediatR;
using PraktikaApplication.TrailerHandlers.Commands;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities.TransportEntities;

namespace PraktikaApplication.TrailerHandlers.Handlers
{
    public class TrailerAddHandler : IRequestHandler<TrailerAddCommand, int>
    {
        private readonly AppDbContext _context;

        public TrailerAddHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(TrailerAddCommand request, CancellationToken cancellationToken)
        {
            if (_context.Trailers.FirstOrDefault(x => x.Number == request.Number) != null)
                throw new Exception("This trailer already exist");

            var Trailer = new Trailer()
            {
                Number = request.Number,
                Status = true,
                TrailerType = request.TrailerType,
            };

            _context.Trailers.Add(Trailer);
            await _context.SaveChangesAsync();

            return Trailer.Id;
        }
    }
}
