using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace Application
{
    public class HotelBookingService : IHotelBookingService
    {
        public List<Hotel> SearchHotels(string city, DateTime startDate, DateTime endDate)
        {
            List<Hotel> availableHotels = new List<Hotel>();

            try
            {
                // Load hotels data from Hotels.xml
                XmlDocument hotelDoc = new XmlDocument();
                hotelDoc.Load(HttpContext.Current.Server.MapPath("~/Services/BookHotelService/Hotels.xml"));

                XmlNodeList hotelNodes = hotelDoc.GetElementsByTagName("Hotel");

                // Load reservation data from Reservations.xml
                XmlDocument reservationDoc = new XmlDocument();
                reservationDoc.Load(HttpContext.Current.Server.MapPath("~/Services/BookHotelService/Reservations.xml"));

                XmlNodeList reservationNodes = reservationDoc.GetElementsByTagName("Reservation");

                // Collect all booked hotels for the specific date range
                HashSet<string> bookedHotels = new HashSet<string>();
                foreach (XmlNode reservationNode in reservationNodes)
                {
                    string hotelName = reservationNode.SelectSingleNode("HotelName").InnerText;
                    DateTime reservationStartDate = DateTime.Parse(reservationNode.SelectSingleNode("StartDate").InnerText);
                    DateTime reservationEndDate = DateTime.Parse(reservationNode.SelectSingleNode("EndDate").InnerText);

                    // Check if the reservation overlaps with the search dates
                    if ((startDate <= reservationEndDate) && (endDate >= reservationStartDate))
                    {
                        bookedHotels.Add(hotelName);  // Mark hotel as booked
                    }
                }

                // Now, filter hotels based on the city and availability
                foreach (XmlNode hotelNode in hotelNodes)
                {
                    XmlNode cityNode = hotelNode.SelectSingleNode("Address/City");
                    if (cityNode != null && cityNode.InnerText.Equals(city, StringComparison.OrdinalIgnoreCase))
                    {
                        string hotelName = hotelNode.SelectSingleNode("Name").InnerText;
                        if (!bookedHotels.Contains(hotelName))  // Only include hotels that are not booked
                        {
                            Hotel hotel = new Hotel
                            {
                                Name = hotelName,
                                Rating = hotelNode.Attributes["Rating"]?.Value,
                                Phone = hotelNode.SelectNodes("Phone").Cast<XmlNode>().Select(n => n.InnerText).ToList(),
                                Address = new Address
                                {
                                    Number = hotelNode.SelectSingleNode("Address/Number").InnerText,
                                    Street = hotelNode.SelectSingleNode("Address/Street").InnerText,
                                    City = cityNode.InnerText,
                                    State = hotelNode.SelectSingleNode("Address/State").InnerText,
                                    Zip = hotelNode.SelectSingleNode("Address/Zip").InnerText,
                                    NearstAirport = hotelNode.SelectSingleNode("Address/@NearstAirport")?.Value
                                }
                            };

                            availableHotels.Add(hotel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors here
                throw new Exception("Error while searching hotels: " + ex.Message);
            }

            return availableHotels;
        }

        public string BookHotel(string hotelName, DateTime startDate, DateTime endDate, string customerName)
        {
            try
            {
                // Load reservation data from Reservations.xml
                XmlDocument reservationDoc = new XmlDocument();
                reservationDoc.Load(HttpContext.Current.Server.MapPath("~/Services/BookHotelService/Reservations.xml"));

                // Create a new reservation node
                XmlNode reservationNode = reservationDoc.CreateElement("Reservation");

                XmlNode hotelNameNode = reservationDoc.CreateElement("HotelName");
                hotelNameNode.InnerText = hotelName;
                reservationNode.AppendChild(hotelNameNode);

                XmlNode startDateNode = reservationDoc.CreateElement("StartDate");
                startDateNode.InnerText = startDate.ToString("yyyy-MM-dd");
                reservationNode.AppendChild(startDateNode);

                XmlNode endDateNode = reservationDoc.CreateElement("EndDate");
                endDateNode.InnerText = endDate.ToString("yyyy-MM-dd");
                reservationNode.AppendChild(endDateNode);

                XmlNode customerNode = reservationDoc.CreateElement("CustomerName");
                customerNode.InnerText = customerName;
                reservationNode.AppendChild(customerNode);

                // Append the reservation to the XML document
                reservationDoc.DocumentElement.AppendChild(reservationNode);

                // Save the updated Reservations.xml file
                reservationDoc.Save(HttpContext.Current.Server.MapPath("~/Services/BookHotelService/Reservations.xml"));

                return "Booking successful for " + hotelName + " from " + startDate.ToString("yyyy-MM-dd") + " to " + endDate.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                return "Error while booking hotel: " + ex.Message;
            }
        }

    }

    public class Hotel
    {
        public string Name { get; set; }
        public string Rating { get; set; }
        public List<string> Phone { get; set; } // Assuming this is a list of phone numbers
        public Address Address { get; set; }
    }

    public class Address
    {
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string NearstAirport { get; set; }
    }

}