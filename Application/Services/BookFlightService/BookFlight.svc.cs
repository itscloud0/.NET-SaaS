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
        // List of valid IATA codes for US airports (locations)
        private static readonly HashSet<string> ValidLocations = new HashSet<string>
        {
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

        public string BookFlightFunction(int time, string depart, string arrival)
        {
            if (!ValidLocations.Contains(depart.ToUpper()))
            {
                return $"Error: '{depart}' is not a valid location.";
            }

            if (!ValidLocations.Contains(arrival.ToUpper()))
            {
                return $"Error: '{arrival}' is not a valid location.";
            }

            return "Flight Booked Successfully!";
        }
    }
}
