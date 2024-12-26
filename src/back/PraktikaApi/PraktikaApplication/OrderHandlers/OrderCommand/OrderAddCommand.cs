using MediatR;

namespace PraktikaApplication.OrderHandlers.OrderCommand
{
    public class OrderAddCommand : IRequest<int>
    {
        public string OrderName { get; set; }
        public string CargoDescription { get; set; }
    }
}
