using MediatR;

namespace PraktikaApplication.ClientHandlers.ClientCommand
{
    public class ClientEnterCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
