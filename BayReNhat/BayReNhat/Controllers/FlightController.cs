using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
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
		}

		[HttpGet]
		public string OneWayRegister(string from, string to, DateTime departDate,
			string departFlightNo, string firstName, string lastName, string tel, string email)
		{ 
			FlightRegisterParameters parameters = new FlightRegisterParameters();
			parameters.BookingType = "oneway";
			parameters.From = from;
			parameters.To = to;
			parameters.DepartDay = departDate.Day.ToString();
			parameters.DepartMonth = departDate.Month.ToString();
			parameters.DepartYear = departDate.Year.ToString();
			parameters.DepartFlightNo = departFlightNo;
			parameters.FirstName = firstName;
			parameters.LastName = lastName;
			parameters.Phone = tel;
			parameters.Email = email;

			RegisterResultFactory registerResultFactory = new RegisterResultFactory();
			string json = registerResultFactory.GenerateJsonResult(parameters);
			return json;
		}

		[HttpGet]
		public string RoudTripRegister(string from, string to, DateTime departDate, DateTime returnDate,
			string departFlightNo, string returnFlightNo, string firstName, string lastName, string tel, string email)
		{
			FlightRegisterParameters parameters = new FlightRegisterParameters();
			parameters.BookingType = "roundtrip";
			parameters.From = from;
			parameters.To = to;
			parameters.DepartDay = departDate.Day.ToString();
			parameters.DepartMonth = departDate.Month.ToString();
			parameters.DepartYear = departDate.Year.ToString();
			parameters.ReturnDay = returnDate.Day.ToString();
			parameters.ReturnMonth = returnDate.Month.ToString();
			parameters.ReturnYear = returnDate.Year.ToString();
			parameters.DepartFlightNo = departFlightNo;
			parameters.ReturnFlightNo = returnFlightNo;
			parameters.FirstName = firstName;
			parameters.LastName = lastName;
			parameters.Phone = tel;
			parameters.Email = email;

			RegisterResultFactory registerResultFactory = new RegisterResultFactory();
			string json = registerResultFactory.GenerateJsonResult(parameters);
			return json;
		}
    }
}
