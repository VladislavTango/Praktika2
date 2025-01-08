using PraktikaDomain.Enums;

namespace PraktikaDomain.Entities
{
    public class Contract : BaseEntity
    {
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public string ContractTerms { get; set; }
        public ContractType ContractType { get; set; }
        public DateTime ContractDate { get; set; }  
        public DateTime ExpirationDate { get; set; }
    }
}
