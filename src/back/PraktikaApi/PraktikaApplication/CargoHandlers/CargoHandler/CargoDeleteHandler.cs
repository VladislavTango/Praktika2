using CommonShared.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.CargoHandlers.CargoCommand;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.CargoHandlers.CargoHandler
{
    public class CargoDeleteHandler : IRequestHandler<CargoDeleteCommand, int>
    {
        private readonly AppDbContext _context;
        public CargoDeleteHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CargoDeleteCommand request, CancellationToken cancellationToken)
        {
            var cargo = await _context.Cargos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (cargo == null)
                throw new Exception("Cargo not found");

            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();

            return request.Id;
        }
    }
}
