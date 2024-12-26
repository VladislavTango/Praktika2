using MediatR;

namespace PraktikaApplication.TransportationHandlers.TransportationCommand
{
    public class TransportationDeleteCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
