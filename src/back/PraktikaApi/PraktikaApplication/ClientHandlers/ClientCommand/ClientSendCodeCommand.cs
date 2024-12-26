using MediatR;

namespace PraktikaApplication.ClientHandlers.ClientCommand
{
    public class ClientSendCodeCommand : IRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
