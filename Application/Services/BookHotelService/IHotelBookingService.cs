using System;
using System.ServiceModel;

namespace Application
{
    [ServiceContract]
    public interface IHotelBookingService
    {
        [OperationContract]
        string BookHotel(string city, DateTime date);
    }
}
