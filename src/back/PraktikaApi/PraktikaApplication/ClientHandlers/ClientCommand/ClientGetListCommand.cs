using MediatR;
using PraktikaDomain.Entities;

namespace PraktikaApplication.ClientHandlers.ClientCommand
{
    public class ClientGetListCommand : IRequest<List<Client>>
    {
        public int PageLength { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; } = "Id";
    }
}
