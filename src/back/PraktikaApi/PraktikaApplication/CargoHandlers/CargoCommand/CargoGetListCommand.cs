using MediatR;
using PraktikaDomain.Entities;

namespace PraktikaApplication.CargoHandlers.CargoCommand
{
    public class CargoGetListCommand : IRequest<List<Cargo>>
    {
        public int PageLength { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; } = "Id";
    }
}
