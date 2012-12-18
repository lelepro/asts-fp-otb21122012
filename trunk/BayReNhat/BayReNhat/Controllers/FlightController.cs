using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BayReNhat.Models;
using Newtonsoft.Json;

namespace BayReNhat.Controllers
{
    public class FlightController : ApiController
    {
		[HttpGet]
		public string Search(FlightSearchModel model)
		{
			SearchResultModel result = new SearchResultModel()
				{
					BookingType = BookingType.OneWay,
					Data = new DirectionModel[] {
						new DirectionModel()
							{
								DepartCity = "Ha Noi",
								ReturnCity = "TP HCM",
								Date = "12/18/2012",
								Flights = new FlightModel[]
								{
									new FlightModel("Vietnam Airline", "VN 1374", "16:55", "18:15", 814000, "VND"),
									new FlightModel("Vietnam Airline", "VN 1370", "05:50", "07:10", 814000, "VND")
								}
							},
						new DirectionModel()
							{
								DepartCity = "TP HCM",
								ReturnCity = "Ha Noi",
								Date = "12/19/2012",
								Flights = new FlightModel[]
								{
									new FlightModel("Vietnam Airline", "VN 1374", "16:55", "18:15", 814000, "VND"),
									new FlightModel("Vietnam Airline", "VN 1370", "05:50", "07:10", 814000, "VND")
								}
							}
					}
					
				};
			
			return JsonConvert.SerializeObject(result, Formatting.None);
		}
    }
}
