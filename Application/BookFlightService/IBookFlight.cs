using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Application
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBookFlight" in both code and config file together.
    [ServiceContract]
    public interface IBookFlight
    {
        [OperationContract]
        string BookFlightFunction(int time, string depart, string arrival);
    }
}
