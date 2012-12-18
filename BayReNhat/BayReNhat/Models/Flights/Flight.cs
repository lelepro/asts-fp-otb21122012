using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Flights
{
    class Flight
    {
        private string _flightNo;
        private string _airlineName;
        private string _endTime;
        private string _startTime;
        private string _price;

        public string AirlineName
        {
            get { return _airlineName; }
            set { _airlineName = value.Trim(); }
        }

        public string FlightNo
        {
            set { _flightNo = value.Trim(); }
            get { return _flightNo; }
        }

        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value.Trim(); }
        }

        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value.Trim(); }
        }
        public string Price
        {
            get { return _price; }
            set
            {
                _price = Regex.Replace(value, "[^0-9]", ""); ;
            }
        }
    }
}
