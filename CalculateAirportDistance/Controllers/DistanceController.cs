using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CalculateAirportDistance.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DistanceController : ControllerBase
    {
        [HttpGet("distance/{from}/{to}")]
        public async Task<Response> GetDistance(string from, string to)
        {
            var fromAirport = await GetAirportDetails(from);
            var toAirport = await GetAirportDetails(to);

            var response = new Response
            {
                AirportFrom = fromAirport.Name,
                AirportTo = toAirport.Name,
                Distance = CalculateDistance(fromAirport.Location.Lat, fromAirport.Location.Lon, toAirport.Location.Lat, toAirport.Location.Lon)
            };
            return response;
        }

        private static async Task<Airport> GetAirportDetails(string iataCode)
        {
            var client = new RestClient("https://places-dev.cteleport.com");
            var request = new RestRequest($"airports/{iataCode}");
            var response = await client.ExecuteAsync<Airport>(request);
            return response.Data;
        }

        private static decimal CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            const decimal earthRadius = 3958.8m;
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = earthRadius * (decimal)c;
            return Math.Round(distance, 2);
        }

        private static double ToRadians(decimal degrees)
        {
            return (double)(degrees * (decimal)Math.PI / 180);
        }
    }
}