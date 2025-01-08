using MediatR;
using PraktikaDomain.Enums;

namespace PraktikaApplication.VehicleHandlers.Commands
{
    public class GetVehiclesByCargoCommand : IRequest<List<int>>
    {
        public CargoType CargoType { get; set; }
    }
}
