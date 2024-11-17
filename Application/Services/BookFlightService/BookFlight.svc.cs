// Developed by Cole Eastman
// This WCF service provides functionality to book flights, verifying valid departure and arrival airport codes.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Xml;
using EncryptionDecryption;


namespace Application
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookFlight" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookFlight.svc or BookFlight.svc.cs at the Solution Explorer and start debugging.
    public class BookFlight : IBookFlight
    {
        // List of valid IATA codes for US airports (locations).
        private static readonly HashSet<string> ValidLocations = new HashSet<string>
        {
            // IATA airport codes for major US airports.
            "ATL", "LAX", "ORD", "DFW", "DEN", "JFK", "SFO", "SEA", "LAS", "MCO",
            "MIA", "CLT", "PHX", "IAH", "HOU", "BOS", "MSP", "FLL", "DTW", "PHL", "BWI",
            "SLC", "SAN", "DCA", "IAD", "HNL", "STL", "PDX", "TPA", "SJC", "BNA", "OAK",
            "AUS", "CLE", "SMF", "MCI", "IND", "MSY", "RDU", "MKE", "SAT", "PIT", "CMH",
            "CVG", "ONT", "JAX", "PBI", "MEM", "SDF", "RIC", "RNO", "ANC", "BUF", "ABQ",
            "BDL", "OMA", "TUS", "TUL", "ELP", "BOI", "GEG", "BHM", "TYS", "DSM", "GSP",
            "FAT", "LIT", "CHS", "GRR", "DAY", "SYR", "MDT", "ROC", "MHT", "GSO", "PVD",
            "ICT", "MSN", "PNS", "LBB", "SBN", "SGF", "SAV", "ABE", "MYR", "BTV", "AGS",
            "CHA", "MOB", "PHF", "RSW", "AVL", "CAK", "CAE", "SNA", "FSM", "GPT", "MGM",
            "SHV", "FWA", "TOL", "ISP"
        };

        // Method to book a flight, verifying the validity of departure and arrival airport codes.
         public string BookFlightFunction(int time, string depart, string arrival)
        {
            try
            {
                // flight validation
                if (!ValidLocations.Contains(depart.ToUpper()))
                {
                    return $"Error: '{depart}' is not a valid location.";
                }

                if (!ValidLocations.Contains(arrival.ToUpper()))
                {
                    return $"Error: '{arrival}' is not a valid location.";
                }

                // load Flights.xml file
                string xmlFilePath = HttpContext.Current.Server.MapPath("~/Services/BookFlightService/Flights.xml");
                XmlDocument flightDoc = new XmlDocument();

                if (System.IO.File.Exists(xmlFilePath))
                {
                    flightDoc.Load(xmlFilePath);
                }
                else
                {
                    // new XML structure if the file doesn't exist
                    XmlDeclaration xmlDeclaration = flightDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlNode rootNode = flightDoc.CreateElement("Flights");
                    flightDoc.AppendChild(xmlDeclaration);
                    flightDoc.AppendChild(rootNode);
                }

                // flight reservation node
                XmlNode flightNode = flightDoc.CreateElement("Flight");

                XmlNode timeNode = flightDoc.CreateElement("Time");
                timeNode.InnerText = time.ToString();
                flightNode.AppendChild(timeNode);

                XmlNode departNode = flightDoc.CreateElement("Departure");
                departNode.InnerText = depart.ToUpper();
                flightNode.AppendChild(departNode);

                XmlNode arrivalNode = flightDoc.CreateElement("Arrival");
                arrivalNode.InnerText = arrival.ToUpper();
                flightNode.AppendChild(arrivalNode);

                // append new flight reservation to the XML document
                flightDoc.DocumentElement.AppendChild(flightNode);

                flightDoc.Save(xmlFilePath);

                return $"Flight booked successfully!";
            }
            catch (Exception ex)
            {
                return $"Error while booking flight: {ex.Message}";
            }
        }
    }
}
