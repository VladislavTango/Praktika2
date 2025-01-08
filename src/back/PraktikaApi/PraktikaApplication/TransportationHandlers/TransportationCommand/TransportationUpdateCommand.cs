using MediatR;
using PraktikaDomain.Entities.TransportEntities;
using PraktikaDomain.Enums;

namespace PraktikaApplication.TransportationHandlers.TransportationCommand
{
    public class TransportationUpdateCommand : IRequest
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string OrderNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Road {  get; set; }
        public CargoType CargoType { get; set; }
        public int VehicleId { get; set; }
        public TransportationStatus TransportationStatus { get; set; }
    }
}
