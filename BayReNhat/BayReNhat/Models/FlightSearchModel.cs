using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BayReNhat.Models
{
	public enum BookingType
	{
		OneWay = 1,
		RoundTrip = 2,
		MultiDest = 3
	}

	public class FlightSearchModel
	{
		public BookingType BookingType { get; set; }
		public string DepartCity { get; set; }
		public string ReturnCity { get; set; }
		public DateTime DepartDay { get; set; }
		public DateTime ReturnDay { get; set; }
		public int NoOfAdult { get; set; }
		public int NoOfChildren { get; set; }
		public int NoOfInfant { get; set; }
		public string GetBookingTypeName()
		{
			switch (BookingType)
			{
				case BookingType.RoundTrip:
					return "Khứ hồi";
				case BookingType.OneWay:
					return "Một chiều";
				case BookingType.MultiDest:
					return "Nhiều chặng";
			}
			return "";
		}
	}
}