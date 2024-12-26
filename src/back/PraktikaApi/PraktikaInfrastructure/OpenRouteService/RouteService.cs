using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PraktikaDomain.Interfaces;

namespace PraktikaInfrastructure.OpenRouteService
{
    public class RouteService : IRouteService
    {
        private const string apiKey = "5b3ce3597851110001cf624849a6956f84104a11ab38ab6ab0a29970";

        private const string GpsKey = "GAvyjpTWlypUBaU5eHZ5AlrE27dDzsrV";

        private readonly HttpClient _httpClient;

        public RouteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(double Latitude, double Longitude)> GetCoordinates(string placeName)
        {
            string url = $"https://api.openrouteservice.org/geocode/search?api_key={apiKey}&text={Uri.EscapeDataString(placeName)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch coordinates: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var responseData = JObject.Parse(json);

            var coordinates = responseData["features"]?.FirstOrDefault()?["geometry"]?["coordinates"] as JArray;

            if (coordinates != null && coordinates.Count == 2)
            {
                return (Latitude: (double)coordinates[1], Longitude: (double)coordinates[0]);
            }

            throw new Exception("Сoordinates not returned");
        }

        public async Task<string> Gps(double[] startPoint, double[] endPoint)
        {
            string Start = (startPoint[1].ToString().Replace(",", ".") + "," + startPoint[0].ToString().Replace(",", "."));
            string End = (endPoint[1].ToString().Replace(",", ".") + "," + endPoint[0].ToString().Replace(",", "."));

            string url = $"https://api.tomtom.com/routing/1/calculateRoute/{Start}:{End}/json?key={GpsKey}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                var json = JObject.Parse(responseContent);

                return json.ToString();
            }
            else 
            {
                throw new Exception($"{response.StatusCode} {response.Content.ToString()}");
            }
        }
    }
}