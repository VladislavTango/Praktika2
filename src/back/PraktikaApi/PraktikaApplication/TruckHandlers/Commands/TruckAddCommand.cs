using MediatR;

namespace PraktikaApplication.TruckHandlers.Commands
{
    public class TruckAddCommand : IRequest<int>
    {
        public string Number { get; set; }
        public string Mark {  get; set; }

    }
}
