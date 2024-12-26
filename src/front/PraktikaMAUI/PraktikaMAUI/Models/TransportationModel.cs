using PraktikaMAUI.Components.Interface;
using PraktikaMAUI.Models.Enum;

namespace PraktikaMAUI.Models
{
    public class TransportationModel : IHasDateRange
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int OrderId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Road { get; set; }
        public int TransportationStatus { get; set; }
    }
}
