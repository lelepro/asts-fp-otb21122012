using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    class FlightRegisterParameters : Parameters
    {

        public string DepartFlightNo { get; set; }
        public string ReturnFlightNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email{ get; set; }
    }
}
