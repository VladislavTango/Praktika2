using PraktikaMAUI.Models.Enum;
namespace PraktikaMAUI.Models
{
    public class ContractModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public bool Status { get; set; }
        public string ContractTerms { get; set; }
        public ContractType ContractType { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
