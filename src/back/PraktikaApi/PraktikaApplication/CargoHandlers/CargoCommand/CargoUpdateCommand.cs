using MediatR;

namespace PraktikaApplication.CargoHandlers.CargoCommand
{
    public class CargoUpdateCommand : IRequest
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public int TransportationId { get; set; }
    }
}
