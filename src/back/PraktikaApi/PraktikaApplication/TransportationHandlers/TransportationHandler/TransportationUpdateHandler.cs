using AutoMapper;
using CommonShared.Domains;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.TransportationHandlers.TransportationCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;
using PraktikaDomain.Entities.TransportEntities;
using PraktikaDomain.Enums;
using PraktikaDomain.Interfaces;
using System.Diagnostics;

namespace PraktikaApplication.TransportationHandlers.TransportationHandler
{
    public class TransportationUpdateHandler : IRequestHandler<TransportationUpdateCommand>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokentService _jwtTokentService;

        public TransportationUpdateHandler(AppDbContext context, IMapper mapper, IEmailService emailService, IHttpContextAccessor httpContextAccessor, IJwtTokentService jwtTokentService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokentService = jwtTokentService;
        }

        public async Task Handle(TransportationUpdateCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];

            var claims = _jwtTokentService.TokenClaimCatcher(authorizationHeader);

            var transportation = await _context.Transportations.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (transportation == null)
                throw new Exception("Transportation not found");

            var vehicle = await _context.Vehicles
                .Include(x => x.Truck)
                .Include(x => x.Trailer)
                .FirstOrDefaultAsync(x => x.Id == request.VehicleId);

            if (vehicle == null)
                throw new Exception("Vehicle not found");

            var isCompatible = await _context.cargoTrailerCompatibilities
                .AnyAsync(x => x.CargoType == request.CargoType && x.TrailerType == vehicle.Trailer.TrailerType);

            if (!isCompatible)
                throw new Exception("The types do not match");

            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderNumber == request.OrderNumber);

            if (order == null)
                throw new Exception("Order not found");

            transportation.OrderId = order.Id;

            if (transportation.TransportationStatus != request.TransportationStatus)
            {
                await _emailService.SendTransportationStatus(request.TransportationStatus.ToString(), claims.userEmail);

                if (request.TransportationStatus == TransportationStatus.READY)
                {
                    var invoice = new Invoices
                    {
                        TransportationId = request.Id,
                        Status = false
                    };
                    _context.Inovices.Add(invoice);
                }
                else
                {
                    var invoice = await _context.Inovices.FirstOrDefaultAsync(x => x.TransportationId == transportation.Id);
                    if (invoice != null)
                        _context.Inovices.Remove(invoice);
                }
            }

            _mapper.Map(request, transportation);
            await _context.SaveChangesAsync();
        }

    }
}
