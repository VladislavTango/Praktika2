using CommonShared.Domains;
using MediatR;
using PraktikaApplication.ClientHandlers.ClientCommand;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.ClientHandlers.ClientHandler
{
    public class ClientDeleteHadler : IRequestHandler<ClientDeleteCommand, int>
    {
        private readonly AppDbContext _context;

        public ClientDeleteHadler(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> Handle(ClientDeleteCommand request, CancellationToken cancellationToken)
        {
            var client = _context.Clients.FirstOrDefault(x=> x.Id == request.Id);

            if (client == null)
                throw new Exception("Client not found");


            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return client.Id;
        }
    }
}
