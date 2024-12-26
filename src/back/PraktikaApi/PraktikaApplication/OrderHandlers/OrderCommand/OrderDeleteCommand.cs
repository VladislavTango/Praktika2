using MediatR;

namespace PraktikaApplication.OrderHandlers.OrderCommand
{
    public class OrderDeleteCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
