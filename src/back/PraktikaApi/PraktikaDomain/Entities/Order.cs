namespace PraktikaDomain.Entities
{
    public class Order : BaseEntity
    {
        public int ClientId { get; set; }
        public Client Client {  get; set; }
        public string OrderName { get; set; }
        public string CargoDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ICollection<Transportation> TransportationList { get; set; } = new List<Transportation>();
        public ICollection<Contract> ContractList { get; set; } = new List<Contract>();

    }
}
