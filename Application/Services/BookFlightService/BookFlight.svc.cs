// Developed by Cole Eastman
// This WCF service provides functionality to book flights, verifying valid departure and arrival airport codes.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
            // Check if the departure location is a valid IATA code.
            if (!ValidLocations.Contains(depart.ToUpper()))
            {
                return $"Error: '{depart}' is not a valid location."; // Return an error message if invalid.
            }

            // Check if the arrival location is a valid IATA code.
            if (!ValidLocations.Contains(arrival.ToUpper()))
            {
                return $"Error: '{arrival}' is not a valid location."; // Return an error message if invalid.
            }

            // If both locations are valid, return a success message.
            return "Flight Booked Successfully!";
        }
    }
}
