using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BayReNhat.Models
{
	public enum BookingType
	{
		RoundTrip,
		OneWay,
		MultiDest
	}

	public class FlightSearchModel
	{
		[Required]
		[EnumDataType(typeof(BookingType))]
		public BookingType BookingType { get; set; }
		[Required]
		public string DepartCity { get; set; }
		[Required]
		public string ReturnCity { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateTime DepartDay { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateTime ReturnDay { get; set; }
		[Required]
		[Range(1,6)]
		public int NoOfAdult { get; set; }
		[Required]
		[Range(0,3)]
		public int NoOfChildren { get; set; }
		[Required]
		[Range(0,2)]
		public int NoOfInfant { get; set; }
	}
}