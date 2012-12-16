using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    class ReturnData
    {
        public string BookingType { get; set; }
        public List<Trip> Data { get; set; }

        public static ReturnData CreateReturnData(string bookingType)
        {
            ReturnData returnData = new ReturnData();
            returnData.BookingType = bookingType;
            returnData.Data = new List<Trip>();

            return returnData;
        }
    }
}
