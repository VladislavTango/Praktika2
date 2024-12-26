using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.InvoiceHandlers.Commands;
using PraktikaApplication.InvoiceHandlers.Response;
using PraktikaApplication.OrderHandlers.Response;
using PraktikaApplication.TransportationHandlers.Response;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.InvoiceHandlers.Handlers
{
    public class InvoiceGetInfoHandler : IRequestHandler<InvoiceGetInfoCommand, InvoiceGetInfoResponse>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceGetInfoHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }

        public async Task<InvoiceGetInfoResponse> Handle(InvoiceGetInfoCommand request, CancellationToken cancellationToken)
        {

            var Transportations = await _context.Transportations
                                .FirstOrDefaultAsync(x => x.Id == request.TransportationId);
            var Order = await _context.Orders
                                .FirstOrDefaultAsync(x => x.Id == Transportations.OrderId);

            var response = new InvoiceGetInfoResponse()
            {
                InvoiceId = request.Id,
                Transportation = _mapper.Map<TransportationGetListResponse>(Transportations),
                Order = _mapper.Map<OrderGetListResponse>(Order)
            };
            return response;
        }
    }
}
