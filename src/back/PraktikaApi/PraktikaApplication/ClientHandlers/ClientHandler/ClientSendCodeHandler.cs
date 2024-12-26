using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ClientHandlers.ClientCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Interfaces;

namespace PraktikaApplication.ClientHandlers.ClientHandler
{
    public class ClientSendCodeHandler : IRequestHandler<ClientSendCodeCommand>
    {
        private readonly IMailRepository _mailRepository;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;

        public ClientSendCodeHandler(IMailRepository mailRepository, AppDbContext context , IEmailService emailService)
        {
            _mailRepository = mailRepository;
            _context = context;
            _emailService = emailService;
        }

        public async Task Handle(ClientSendCodeCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Clients.FirstOrDefaultAsync(x => x.Name == request.Name) != null)
                throw new Exception("Client already exist");

            int Code = await _mailRepository.AddMailCode(request.Email);

            await _emailService.SendConfirmCode(request.Email,Code);
        }
    }
}
