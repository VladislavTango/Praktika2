using AutoMapper;
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.InovicesHandlers.Response;
using PraktikaApplication.InvoicesHandlers.Commands;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.InvoicesHandlers.Handlers
{
    public class InvoicesGetListHandler : IRequestHandler<InvoicesGetListCommand, List<InvoicesGetListResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InvoicesGetListHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<InvoicesGetListResponse>> Handle(InvoicesGetListCommand request, CancellationToken cancellationToken)
        {
            var gridifyQuery = new GridifyQuery()
            {
                OrderBy = request.SortBy,
            };

            var Invoices = await _context.Inovices
            .ApplyFiltering(gridifyQuery)
            .ApplyOrdering(gridifyQuery)
            .Skip((request.PageNumber - 1) * request.PageLength)
            .Take(request.PageLength)
            .ToListAsync(cancellationToken);

            return _mapper.Map<List<InvoicesGetListResponse>>(Invoices);
        }
    }
}
