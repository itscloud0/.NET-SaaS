// Developed by Ilia Sorokin
// This WCF service allows users to search for hotels and book reservations. 
// It integrates encryption and decryption for sensitive customer data.

using EncryptionDecryption;
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
        // Method to search for hotels by city and date range
        public List<Hotel> SearchHotels(string city, DateTime startDate, DateTime endDate)
        {
            List<Hotel> availableHotels = new List<Hotel>();

            try
            {
                // Load hotel data from the Hotels.xml file
                XmlDocument hotelDoc = new XmlDocument();
                hotelDoc.Load(HttpContext.Current.Server.MapPath("~/Services/BookHotelService/Hotels.xml"));

                // Retrieve hotel nodes from the XML document
                XmlNodeList hotelNodes = hotelDoc.GetElementsByTagName("Hotel");

                // Load reservation data from the Reservations.xml file
                XmlDocument reservationDoc = new XmlDocument();
                reservationDoc.Load(HttpContext.Current.Server.MapPath("~/Services/BookHotelService/Reservations.xml"));

                // Retrieve reservation nodes from the XML document
                XmlNodeList reservationNodes = reservationDoc.GetElementsByTagName("Reservation");

                // Create a set to track hotels that are already booked for the specified date range
                HashSet<string> bookedHotels = new HashSet<string>();
                foreach (XmlNode reservationNode in reservationNodes)
                {
                    string hotelName = reservationNode.SelectSingleNode("HotelName").InnerText;
                    DateTime reservationStartDate = DateTime.Parse(reservationNode.SelectSingleNode("StartDate").InnerText);
                    DateTime reservationEndDate = DateTime.Parse(reservationNode.SelectSingleNode("EndDate").InnerText);

                    // Check for overlap between reservation and search dates
                    if ((startDate <= reservationEndDate) && (endDate >= reservationStartDate))
                    {
                        bookedHotels.Add(hotelName); // Add hotel to the booked list
                    }

                    // Decrypt customer name for informational purposes
                    string encryptedCustomerName = reservationNode.SelectSingleNode("CustomerName").InnerText;
                    string decryptedCustomerName = EncDec.Decrypt(encryptedCustomerName);

                    // Display decrypted customer name (optional)
                    Console.WriteLine($"Customer: {decryptedCustomerName}");
                }

                // Filter hotels based on city and availability
                foreach (XmlNode hotelNode in hotelNodes)
                {
                    XmlNode cityNode = hotelNode.SelectSingleNode("Address/City");
                    if (cityNode != null && cityNode.InnerText.Equals(city, StringComparison.OrdinalIgnoreCase))
                    {
                        string hotelName = hotelNode.SelectSingleNode("Name").InnerText;
                        if (!bookedHotels.Contains(hotelName)) // Include only non-booked hotels
                        {
                            Hotel hotel = new Hotel
                            {
                                Name = hotelName,
                                Rating = hotelNode.Attributes["Rating"]?.Value, // Retrieve hotel rating
                                Phone = hotelNode.SelectNodes("Phone").Cast<XmlNode>().Select(n => n.InnerText).ToList(), // Retrieve phone numbers
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
                // Handle any exceptions during the search process
                throw new Exception("Error while searching hotels: " + ex.Message);
            }

            return availableHotels; // Return the list of available hotels
        }

        // Method to book a hotel and store reservation data
        public string BookHotel(string hotelName, DateTime startDate, DateTime endDate, string customerName)
        {
            try
            {
                // Encrypt customer name before saving to reservations
                string encryptedCustomerName = EncDec.Encrypt(customerName);

                // Load reservation data from Reservations.xml
                XmlDocument reservationDoc = new XmlDocument();
                reservationDoc.Load(HttpContext.Current.Server.MapPath("~/Services/BookHotelService/Reservations.xml"));

                // Create a new reservation entry
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
                customerNode.InnerText = encryptedCustomerName; // Store encrypted customer name
                reservationNode.AppendChild(customerNode);

                // Append the new reservation to the XML file
                reservationDoc.DocumentElement.AppendChild(reservationNode);

                // Save the updated Reservations.xml file
                reservationDoc.Save(HttpContext.Current.Server.MapPath("~/Services/BookHotelService/Reservations.xml"));

                return "Booking successful for " + hotelName + " from " + startDate.ToString("yyyy-MM-dd") + " to " + endDate.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                // Return an error message if booking fails
                return "Error while booking hotel: " + ex.Message;
            }
        }
    }

    // Class to represent a hotel
    public class Hotel
    {
        public string Name { get; set; }
        public string Rating { get; set; }
        public List<string> Phone { get; set; } // List of phone numbers
        public Address Address { get; set; }
    }

    // Class to represent a hotel's address
    public class Address
    {
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string NearstAirport { get; set; } // Nearest airport to the hotel
    }
}
