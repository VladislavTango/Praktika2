using MediatR;
using PraktikaDomain.Enums;

namespace PraktikaApplication.TrailerHandlers.Commands
{
    public class TrailerAddCommand : IRequest<int>
    {
        public string Number { get; set; }
        public TrailerType TrailerType { get; set; }
    }
}
