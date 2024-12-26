using CommonShared.Domains;
using MediatR;
using PraktikaApplication.TransportationHandlers.TransportationCommand;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.TransportationHandlers.TransportationHandler
{
    public class TransportationDeleteHandler : IRequestHandler<TransportationDeleteCommand, int>
    {
        private readonly AppDbContext _context;
        public TransportationDeleteHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(TransportationDeleteCommand request, CancellationToken cancellationToken)
        {
            var transportation = _context.Transportations.FirstOrDefault(x => x.Id == request.Id);
            if (transportation == null)
                throw new Exception("Contract not found");

            _context.Transportations.Remove(transportation);
            _context.SaveChanges();

            return request.Id;
        }
    }
}
