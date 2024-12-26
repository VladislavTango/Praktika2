using MediatR;

namespace PraktikaApplication.CargoHandlers.CargoCommand
{
    public class CargoDeleteCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
