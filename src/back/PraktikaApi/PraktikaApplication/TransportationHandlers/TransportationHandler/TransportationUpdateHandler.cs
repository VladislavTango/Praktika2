using AutoMapper;
using CommonShared.Domains;
using MediatR;
using Microsoft.AspNetCore.Http;
using PraktikaApplication.TransportationHandlers.TransportationCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain;
using PraktikaDomain.Entities;
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
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];

            var Claims = _jwtTokentService.TokenClaimCatcher(authorizationHeader);

            var transportation = _context.Transportations.FirstOrDefault(x => x.Id == request.Id);

            if (transportation == null)
                throw new Exception("Transportation not found");

            if(transportation.TransportationStatus != request.TransportationStatus) 
            {
                 await _emailService.SendTransportationStatus
                    (request.TransportationStatus.ToString() , Claims.userEmail);
                if (request.TransportationStatus == PraktikaDomain.TransportationStatus.READY)
                {
                    var Invoice = new Invoices()
                    {
                        TransportationId = request.Id,
                        Status = false
                    };
                    _context.Inovices.Add(Invoice);
                }
                else 
                {
                    var Invoice =_context.Inovices.FirstOrDefault(x => x.TransportationId == transportation.Id);
                    if(Invoice != null)
                        _context.Inovices.Remove(Invoice);
                }
            }


            _mapper.Map(request, transportation);

            _context.SaveChanges();
        }
    }
}
