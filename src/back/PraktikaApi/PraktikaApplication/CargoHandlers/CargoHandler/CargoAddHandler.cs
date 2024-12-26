using AutoMapper;
using MediatR;
using PraktikaApplication.CargoHandlers.CargoCommand;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;

namespace PraktikaApplication.CargoHandlers.CargoHandler
{
    public class CargoAddHandler : IRequestHandler<CargoAddCommand, int>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CargoAddHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CargoAddCommand request, CancellationToken cancellationToken)
        {
            var cargo = _mapper.Map<Cargo>(request);

            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();

            return cargo.Id;
        }
    }
}
