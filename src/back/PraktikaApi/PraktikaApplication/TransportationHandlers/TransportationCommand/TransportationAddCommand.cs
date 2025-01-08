using MediatR;
using PraktikaDomain.Enums;

namespace PraktikaApplication.TransportationHandlers.TransportationCommand
{
    public class TransportationAddCommand : IRequest<int>
    {
        public string OrderNumber { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Road {  get; set; }
        public CargoType CargoType { get; set; }
    }
}
