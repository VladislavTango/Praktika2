using System.ComponentModel.DataAnnotations;

namespace PraktikaMAUI.Models.ModalModels
{
    public class AddOrder
    {
        [Required(ErrorMessage = "Input is required.")]
        public string OrderName { get; set; }
        [Required(ErrorMessage = "Input is required.")]
        public string CargoDescription { get; set; }
    }
}
