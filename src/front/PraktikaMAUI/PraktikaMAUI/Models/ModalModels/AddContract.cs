using PraktikaMAUI.Components.Interface;
using PraktikaMAUI.Models.Enum;

namespace PraktikaMAUI.Models.ModalModels
{
    public class AddContract : IHasDateRange
    {
        public string contractTerms { get; set; }
        public ContractType contractType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
