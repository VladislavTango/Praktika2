using MediatR;

namespace PraktikaApplication.OrderHandlers.OrderCommand
{
    public class OrderAddCommand : IRequest<int>
    {
        public int ContractId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string OrderName { get; set; }
        public string CargoDescription { get; set; }
    }
}
