using MediatR;

namespace PraktikaApplication.TransportationHandlers.TransportationCommand
{
    public class TransportationAddCommand : IRequest<int>
    {
        public int OrderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Road {  get; set; }
    }
}
