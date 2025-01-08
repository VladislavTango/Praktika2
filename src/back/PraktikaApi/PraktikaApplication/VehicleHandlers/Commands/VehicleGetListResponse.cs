using MediatR;
using PraktikaApplication.VehicleHandlers.Response;


namespace PraktikaApplication.VehicleHandlers.Commands
{
    public class VehicleGetListCommand : IRequest<List<VehicleGetListResponse>>
    {
        public int PageLength { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; } = "Id";
    }
}
