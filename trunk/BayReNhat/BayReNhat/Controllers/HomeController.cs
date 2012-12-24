using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BayReNhat.Models;

namespace BayReNhat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Search(FlightSearchModel model)
		{
			return View(model);
		}

		[HttpPost]
		public JsonResult Booking(List<FlightModel> flights, string bookingType)
		{
			TempData["flights"] = flights;
			TempData["BookingType"] = bookingType;
			return Json(new {redirect = "booking"});
		}

		[HttpGet]
		public ActionResult Booking()
		{
			return View();
		}
    }
}
