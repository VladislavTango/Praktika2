using AutoMapper;
using MediatR;
using PraktikaApplication.RouteHandlers.RouteCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Interfaces;
namespace PraktikaApplication.RouteHandlers.RouteHandler
{
    public class CalculateRouteHandler : IRequestHandler<CalculateRouteCommand, string>
    {
        private readonly AppDbContext _context;
        private readonly IRouteService _routingService;
        private readonly IMapper _mapper;

        public CalculateRouteHandler(AppDbContext context, IRouteService routingService, IMapper mapper)
        {
            _context = context;
            _routingService = routingService;
            _mapper = mapper;
        }
        public async Task<string> Handle(CalculateRouteCommand request, CancellationToken cancellationToken)
        {
            var StartCoordinates = await _routingService.GetCoordinates(request.StartPointName);

            var EndCoordinates = await _routingService.GetCoordinates(request.EndPointName);

            string Route = await _routingService.Gps(
                [StartCoordinates.Longitude, StartCoordinates.Latitude],
                [EndCoordinates.Longitude, EndCoordinates.Latitude]);

            return Route;
        }
    }
}
