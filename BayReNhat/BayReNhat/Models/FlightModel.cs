using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BayReNhat.Models
{
	public class FlightModel
	{
		public string AirplineName { get; set; }
		public string FlightNo { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public int Price { get; set; }
		public string CurUnit { get; set; }

		public FlightModel(string name, string no, string stime, string etime, int price, string unit)
		{
			AirplineName = name;
			FlightNo = no;
			StartTime = stime;
			EndTime = etime;
			Price = price;
			CurUnit = unit;
		}
	}
}