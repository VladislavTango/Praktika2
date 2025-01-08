using PraktikaMAUI.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PraktikaMAUI.Models.ModalModels
{
    public class AddOrder
    {
        public string contractId {  get; set; }
        public string OrderName { get; set; }
        public string CargoDescription { get; set; }
    }
}
