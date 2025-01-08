using MediatR;

namespace PraktikaApplication.VehicleHandlers.Commands
{
    public class VehicleAddCommand : IRequest<int>
    {
        public string TruckNumber { get; set; }
        public string TrailerNumber { get; set; }
    }
}
