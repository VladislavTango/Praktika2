namespace PraktikaApplication.OrderHandlers.Response
{
    public class OrderGetListResponse
    {
        public string orderName { get; set; }
        public string cargoDescription { get; set; }
        public DateTime createdDate { get; set; }
        public int id { get; set; }
        public bool Status { get; set; }
    }
}
