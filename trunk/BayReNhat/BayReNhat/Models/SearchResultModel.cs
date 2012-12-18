using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BayReNhat.Models
{
	public class SearchResultModel
	{
		public BookingType BookingType { get; set; }
		public IEnumerable<DirectionModel> Data { get; set; }
	}
}