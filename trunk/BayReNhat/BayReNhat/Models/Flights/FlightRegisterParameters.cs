using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    class FlightRegisterParameters : Parameters
    {
        private string _returnFlightNo = "";
        private string _departFlightNo = "";
        private string _firstName = "";
        private string _lastName = "";
        private string _phone = "";
        private string _email = "";

        public string ReturnFlightNo
        {
            get { return _returnFlightNo; }
            set { _returnFlightNo = value; }
        }

        public string DepartFlightNo
        {
            get { return _departFlightNo; }
            set { _departFlightNo = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}
