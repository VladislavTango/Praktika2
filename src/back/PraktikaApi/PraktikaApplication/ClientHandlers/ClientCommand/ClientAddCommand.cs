using MediatR;

namespace PraktikaApplication.ClientHandlers.ClientCommand
{
    public class ClientAddCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Code { get; set; }
        public string Contacts { get; set; }
    }
}
