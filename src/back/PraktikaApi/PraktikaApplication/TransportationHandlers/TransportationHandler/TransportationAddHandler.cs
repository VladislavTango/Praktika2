using AutoMapper;
using CommonShared.Domains;
using MediatR;
using PraktikaApplication.TransportationHandlers.TransportationCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;

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
            if (_context.Orders.FirstOrDefault(x => x.Id == request.OrderId) == null)
                throw new Exception("Order not found");

            var transportation = _mapper.Map<Transportation>(request);

            transportation.Status = true;
            transportation.TransportationStatus = PraktikaDomain.TransportationStatus.NEW;

            _context.Transportations.Add(transportation);
            _context.SaveChanges();

            return transportation.Id;
        }
    }
}
