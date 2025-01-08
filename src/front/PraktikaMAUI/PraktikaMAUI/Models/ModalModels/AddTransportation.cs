using PraktikaMAUI.Components.Interface;
using System.ComponentModel;

namespace PraktikaMAUI.Models.ModalModels
{
    public class AddTransportation : IHasDateRange
    {
        public string OrderNumber { get; set; }
        public int vehicleId { get; set; }
        public string Road { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get ; set; }
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
