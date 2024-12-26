namespace PraktikaMAUI.Models.ModalModels
{
    public class WriteInvoice
    {
        public int InvoiceId { get; set; }
        public OrderModel Order { get; set; }
        public TransportationModel Transportation { get; set; }
    }
}
