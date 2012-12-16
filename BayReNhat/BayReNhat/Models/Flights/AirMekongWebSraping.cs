using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    class AirMekongWebSraping : WebScraping
    {
        public override IWebScrapingDelegate GetWebScrapingDelegate(string bookingType)
        {
            switch (bookingType)
            {
                case "oneway":
                    return new OneWayAirMekongDelegate();
                case "roundtrip":
                    return new RoundTripAirMekongDelegate();
            }
            return null;
        }
    }
}
