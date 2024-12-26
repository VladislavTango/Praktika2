namespace PraktikaApplication.ContractHandlers.ContractResponse
{
    public class ContractGetListResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public bool Status { get; set; }
        public string ContractTerms { get; set; }
    }
}
