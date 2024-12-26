using PraktikaMAUI.Components.Interface;

namespace PraktikaMAUI.Models.ModalModels
{
    public class AddTransportation : IHasDateRange
    {
        public int OrderId { get; set; }
        public string Road { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get ; set; }
    }
}
