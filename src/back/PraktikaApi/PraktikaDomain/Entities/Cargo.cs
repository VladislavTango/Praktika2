namespace PraktikaDomain.Entities
{
    public class Cargo
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public Transportation Transportation { get; set; }
        public int TransportationId { get; set; }
    }
}
