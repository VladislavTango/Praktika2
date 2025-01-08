using PraktikaMAUI.Components.Interface;
using PraktikaMAUI.Models.Enum;
using System.ComponentModel;

namespace PraktikaMAUI.Models
{
    public class TransportationModel : IHasDateRange
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Road { get; set; }
        public int TransportationStatus { get; set; }
        public int VehicleId { get; set; }
        private int _cargoType;
        public int cargoType
        {
            get => _cargoType;
            set
            {
                if (_cargoType != value)
                {
                    _cargoType = value;
                    OnPropertyChanged(nameof(cargoType));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
