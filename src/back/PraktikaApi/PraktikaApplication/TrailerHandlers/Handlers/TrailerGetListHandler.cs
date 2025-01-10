using AutoMapper;
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.TrailerHandlers.Commands;
using PraktikaApplication.TrailerHandlers.Response;
using PraktikaApplication.TruckHandlers.Response;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.TrailerHandlers.Handlers
{
    public class TrailerGetListHandler : IRequestHandler<TrailerGetListCommand, List<TrailerGetListResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TrailerGetListHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TrailerGetListResponse>> Handle(TrailerGetListCommand request, CancellationToken cancellationToken)
        {
            var gridifyQuery = new GridifyQuery
            {
                OrderBy = request.SortBy,
            };

            var trailers = await _context.Trailers.Where(x => x.Vehicle == null)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<TrailerGetListResponse>>(trailers);
        }
    }
}
