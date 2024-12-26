using MediatR;

namespace PraktikaApplication.OrderHandlers.OrderCommand
{
    public class OrderUpdateCommand : IRequest
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public bool Status { get; set; }
        public string CargoDescription { get; set; }
    }
}
