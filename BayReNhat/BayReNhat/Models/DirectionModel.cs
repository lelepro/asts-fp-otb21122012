using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BayReNhat.Models
{
	public class DirectionModel
	{
		public string DepartCity { get; set; }
		public string ReturnCity { get; set; }
		public string Date { get; set; }
		public IEnumerable<FlightModel> Flights { get; set; }
	}
}