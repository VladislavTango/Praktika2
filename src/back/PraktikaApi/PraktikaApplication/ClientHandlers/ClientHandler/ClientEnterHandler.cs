using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ClientHandlers.ClientCommand;
using PraktikaApplication.Static;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Interfaces;

namespace PraktikaApplication.ClientHandlers.ClientHandler
{
    public class ClientEnterHandler : IRequestHandler<ClientEnterCommand, string>
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokentService _jwtTokentService;

        public ClientEnterHandler(AppDbContext context, IJwtTokentService jwtTokentService)
        {
            _context = context;
            _jwtTokentService = jwtTokentService;
        }

        public async Task<string> Handle(ClientEnterCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Name == request.Name);
            if (client == null)
                throw new Exception("Client not found");

            if (!PasswordHasher.VerifyPassword(request.Password.ToString(), client.Password.ToString()))
                throw new Exception("Incorrect password");

            return _jwtTokentService.GenerateToken(client.Id, client.Email);
        }
    }
}
