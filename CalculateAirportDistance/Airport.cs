namespace CalculateAirportDistance
{
    public class Location
    {
        public decimal Lon { get; set; }
        public decimal Lat { get; set; }
    }

    public class Airport
    {
        public string Country { get; set; }
        public string CityIata { get; set; }
        public string Iata { get; set; }
        public string City { get; set; }
        public string TimezoneRegionName { get; set; }
        public string CountryIata { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Type { get; set; }
        public int Hubs { get; set; }
    }

}