using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Application
{
    public class HotelBookingService : IHotelBookingService
    {
        private readonly List<Hotel> hotels;

        public HotelBookingService()
        {
            hotels = LoadHotelsFromXml();
        }

        private List<Hotel> LoadHotelsFromXml()
        {
            var hotelsList = new List<Hotel>();
            string filePath = HttpContext.Current.Server.MapPath("~/Hotels.xml");

            if (File.Exists(filePath))
            {
                XDocument doc = XDocument.Load(filePath);
                hotelsList = doc.Descendants("Hotel")
                    .Select(h => new Hotel
                    {
                        Name = h.Element("Name")?.Value,
                        City = h.Descendants("City").FirstOrDefault()?.Value,
                        Rating = double.TryParse(h.Attribute("Rating")?.Value, out double rating) ? rating : 0,
                        PhoneNumbers = h.Descendants("Phone").Select(p => p.Value).ToList(),
                        Street = h.Descendants("Street").FirstOrDefault()?.Value,
                        Airport = h.Descendants("Address").FirstOrDefault()?.Attribute("NearstAirport")?.Value
                    }).ToList();
            }
            return hotelsList;
        }

        public string BookHotel(string city, DateTime date)
        {
            var availableHotels = hotels
                .Where(h => h.City.Equals(city, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (availableHotels.Count == 0)
            {
                return $"No hotels available in {city} for the selected date.";
            }

            return string.Join(Environment.NewLine, availableHotels.Select(h =>
                $"Hotel: {h.Name}, City: {h.City}, Airport: {h.Airport}, Rating: {h.Rating}, " +
                $"Phone: {string.Join(", ", h.PhoneNumbers)}, Street: {h.Street}"));
        }
    }

    public class Hotel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public double Rating { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public string Street { get; set; }
        public string Airport { get; set; }
    }
}
