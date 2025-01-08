using PraktikaApplication.TrailerHandlers.Response;
using PraktikaApplication.TruckHandlers.Response;

namespace PraktikaApplication.VehicleHandlers.Response
{
    public class VehicleGetListResponse
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public TruckGetListResponse Truck{ get; set; }
        public TrailerGetListResponse Trailer{ get; set; }
    }
}
