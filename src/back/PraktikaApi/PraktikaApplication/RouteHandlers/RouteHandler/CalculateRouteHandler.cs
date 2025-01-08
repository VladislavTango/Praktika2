using MediatR;
using PraktikaApplication.RouteHandlers.RouteCommand;
using PraktikaDomain.Interfaces;
namespace PraktikaApplication.RouteHandlers.RouteHandler
{
    public class CalculateRouteHandler : IRequestHandler<CalculateRouteCommand, string>
    {
        private readonly IRouteService _routingService;

        public CalculateRouteHandler(IRouteService routingService)
        {
            _routingService = routingService;
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
