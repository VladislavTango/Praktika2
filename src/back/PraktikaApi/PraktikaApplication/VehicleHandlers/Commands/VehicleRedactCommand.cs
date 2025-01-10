using MediatR;

namespace PraktikaApplication.VehicleHandlers.Commands
{
    public class VehicleRedactCommand : IRequest
    {
        public int Id { get; set; }
        public string TruckNumber { get; set; }
        public string TrailerNumber { get; set; }
        public bool Status { get; set; }
    }
}
