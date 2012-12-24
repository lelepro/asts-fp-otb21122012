using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BayReNhat.Models
{
	public class FlightModel
	{
		public string AirlineName { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string FlightNo { get; set; }
		public DateTime Date { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public int Price { get; set; }
	}
}