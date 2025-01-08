namespace PraktikaMAUI.Models.VehicleModels
{
    public class VehicleListResponse
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public TruckGetListResponse Truck { get; set; }
        public TrailerGetListResponse Trailer { get; set; }

    }
}
