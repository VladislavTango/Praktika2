using AutoMapper;
using CommonShared.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ClientHandlers.ClientCommand;
using PraktikaApplication.Static;
using PraktikaDataPersistance.ApplicationContext;


namespace PraktikaApplication.ClientHandlers.ClientHandler
{
    public class ClientUpdateHandler : IRequestHandler<ClientUpdateCommand>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;


        public ClientUpdateHandler(IMapper mapper , AppDbContext context) 
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Handle(ClientUpdateCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (existingClient == null)
                throw new Exception("Client not found");

            _mapper.Map(request, existingClient);

            existingClient.Status = request.Status;

            _context.Clients.Update(existingClient);

            await _context.SaveChangesAsync();
        }


    }
}
