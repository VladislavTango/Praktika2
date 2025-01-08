using PraktikaDomain.Enums;

namespace PraktikaApplication.TrailerHandlers.Response
{
    public class TrailerGetListResponse
    {
        public TrailerType TrailerType { get; set; }
        public string Number { get; set; }
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
