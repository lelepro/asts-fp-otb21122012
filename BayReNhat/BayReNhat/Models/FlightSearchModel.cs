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
		public string GetDepartCityCode()
		{
			return GetCityCode(DepartCity);
		}
		public string GetReturnCityCode()
		{
			return GetCityCode(ReturnCity);
		}

		private static string GetCityCode(string city)
		{
			Dictionary<string, string> map = new Dictionary<string, string>();
			map.Add("Hà Nội", "HAN");
			map.Add("Hải Phòng", "HPH");
			map.Add("Điện Biên", "DIN");
			map.Add("Vinh", "VII");
			map.Add("Huế", "HUI");
			map.Add("Đồng Hới", "VHD");
			map.Add("Đà Nẵng", "DAD");
			map.Add("Pleiku", "PXU");
			map.Add("Tuy Hòa", "TBB");
			map.Add("Hồ Chí Minh", "SGN");
			map.Add("Nha Trang", "NHA");
			map.Add("Đà Lạt", "DLI");
			map.Add("Phú Quốc", "PQC");
			map.Add("Tam Kỳ", "VCL");
			map.Add("Qui Nhơn", "UIH");
			map.Add("Cần Thơ", "VCA");
			map.Add("Côn Đảo", "VCS");
			map.Add("Ban Mê Thuột", "BMV");
			map.Add("Rạch Giá", "VKG");
			map.Add("Cà Mau", "CAH");
			return map[city];
		}
	}
}