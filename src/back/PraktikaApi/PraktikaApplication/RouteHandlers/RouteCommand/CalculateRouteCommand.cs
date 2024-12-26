using MediatR;

namespace PraktikaApplication.RouteHandlers.RouteCommand
{
    public class CalculateRouteCommand : IRequest<string>
    {
        public string StartPointName { get; set; }
        public string EndPointName { get; set; }
    }
}
