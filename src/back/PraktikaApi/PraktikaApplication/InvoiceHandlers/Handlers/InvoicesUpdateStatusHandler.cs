using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.InvoicesHandlers.Commands;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.InovicesHandlers.Handlers
{
    public class InvoicesUpdateStatusHandler : IRequestHandler<InvoicesUpdateStatusCommand>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InvoicesUpdateStatusHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(InvoicesUpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var Invoices = await _context.Inovices.FirstOrDefaultAsync(x => x.Id == request.Id);
            Invoices.Status = request.Status;

            await _context.SaveChangesAsync();
        }
    }
}
