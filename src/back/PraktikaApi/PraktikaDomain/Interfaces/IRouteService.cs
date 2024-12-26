
namespace PraktikaDomain.Interfaces
{
    public interface IRouteService
    {
        Task<string> Gps(double[] startPoint, double[] endPoint);
        Task<(double Latitude, double Longitude)> GetCoordinates(string placeName);
    }
}
