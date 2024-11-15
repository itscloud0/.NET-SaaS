using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Application
{
    [ServiceContract]
    public interface IHotelBookingService
    {
        [OperationContract]
        List<Hotel> SearchHotels(string city, DateTime startDate, DateTime endDate);

        //Method for booking
        [OperationContract]
        string BookHotel(string hotelName, DateTime startDate, DateTime endDate, string customerName);
    }
}
