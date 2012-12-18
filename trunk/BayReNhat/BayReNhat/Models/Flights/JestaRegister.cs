using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{

    class JestaRegister : FlightRegister
    {
        public override void Register(FlightRegisterParameters param)
        {
            WebScraping webScraping = new JestaWebScraping();
            IWebScrapingDelegate webScrapingDelegate = webScraping.GetWebScrapingDelegate(param.BookingType);
            webScrapingDelegate.DownloadWebPage(param, "D:\\FindFlights.html");

        }

    }
}
