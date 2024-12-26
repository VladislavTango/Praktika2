using MediatR;
using PraktikaApplication.ClientHandlers.ClientCommand;
using PraktikaApplication.Static;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;
using PraktikaDomain.Interfaces;


namespace PraktikaApplication.ClientHandlers.ClientHandler
{
    public class ClientAddHandler : IRequestHandler<ClientAddCommand, string>
    {
        private readonly AppDbContext _context;
        private readonly IMailRepository _mailRepository;
        private readonly IJwtTokentService _jwtTokentService;

        public ClientAddHandler(AppDbContext appDbContext , IMailRepository mailRepository
            , IJwtTokentService jwtTokentService)
        {
            _context = appDbContext;
            _mailRepository = mailRepository;
            _jwtTokentService = jwtTokentService;
        }

        public async Task<string> Handle(ClientAddCommand request, CancellationToken cancellationToken)
        {
            if (!(await _mailRepository.SearchMailCode(request.Email) == request.Code))
                throw new Exception("Invalid code");

            await _mailRepository.DeleteMailCode(request.Email);

            var client = new Client
            {
                Name = request.Name,
                Password = PasswordHasher.HashPassword(request.Password),
                Email = request.Email,
                Status = true,
                Contacts = request.Contacts
            };
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();

            return _jwtTokentService.GenerateToken(client.Id, client.Email);
        }
    }
}
