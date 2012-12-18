using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BayReNhat.Models;
using Flights;
using Newtonsoft.Json;

namespace BayReNhat.Controllers
{
    public class FlightController : ApiController
    {
		[HttpGet]
		public string Search(string bookingtype, string from, string to, 
			DateTime departdate, DateTime returndate)
		{
			Parameters parameters = new Parameters();
			parameters.BookingType = bookingtype.ToLower();
			parameters.From = from;
			parameters.To = to;
			parameters.DepartDay = departdate.Day.ToString();
			parameters.DepartMonth = departdate.Month.ToString();
			parameters.DepartYear = departdate.Year.ToString();
			parameters.ReturnDay = returndate.Day.ToString();
			parameters.ReturnMonth = returndate.Month.ToString();
			parameters.ReturnYear = returndate.Year.ToString();
			AirlineInfoFactory airlineInfoFactory = new AirlineInfoFactory();
			string json = airlineInfoFactory.GenerateJsonResult(parameters);
			return json;
			//SearchResultModel result = new SearchResultModel()
			//    {
			//        BookingType = BookingType.OneWay,
			//        Data = new DirectionModel[] {
			//            new DirectionModel()
			//                {
			//                    DepartCity = "Ha Noi",
			//                    ReturnCity = "TP HCM",
			//                    Date = "12/18/2012",
			//                    Flights = new FlightModel[]
			//                    {
			//                        new FlightModel("Vietnam Airline", "VN 1374", "16:55", "18:15", 814000, "VND"),
			//                        new FlightModel("Vietnam Airline", "VN 1370", "05:50", "07:10", 814000, "VND")
			//                    }
			//                },
			//            new DirectionModel()
			//                {
			//                    DepartCity = "TP HCM",
			//                    ReturnCity = "Ha Noi",
			//                    Date = "12/19/2012",
			//                    Flights = new FlightModel[]
			//                    {
			//                        new FlightModel("Vietnam Airline", "VN 1374", "16:55", "18:15", 814000, "VND"),
			//                        new FlightModel("Vietnam Airline", "VN 1370", "05:50", "07:10", 814000, "VND")
			//                    }
			//                }
			//        }

			//    };

			//return JsonConvert.SerializeObject(result, Formatting.None);
			//return msg;
		}
    }
}
