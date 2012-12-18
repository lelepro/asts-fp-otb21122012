using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    class Trip
    {
        public string DepartCity { get; set; }
        public string ReturnCity { get; set; }
		public string Date { get; set; } 
        public List<Flight> Flights { get; set; }
    }
}
