using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
	public class Parameters
    {
        public string From { get; set; }

        public string To { get; set; }

        public string DepartDay { get; set; }

        public string DepartMonth { get; set; }

        public string DepartYear { get; set; }

        public string ReturnDay { get; set; }

        public string ReturnMonth { get; set; }

        public string ReturnYear { get; set; }

        public string BookingType { get; set; }
    }
}
