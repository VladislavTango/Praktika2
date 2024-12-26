using MediatR;

namespace PraktikaApplication.ClientHandlers.ClientCommand
{
    public class ClientUpdateCommand : IRequest
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Dictionary<string, string> Contacts { get; set; }

    }
}
