namespace PraktikaDomain.Entities
{
    public class Contract : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ContractTerms { get; set; }

    }
}
