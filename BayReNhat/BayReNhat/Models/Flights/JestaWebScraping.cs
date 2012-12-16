using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    class JestaWebScraping : WebScraping
    {
        public override IWebScrapingDelegate GetWebScrapingDelegate(string bookingType)
        {
            switch (bookingType)
            {
                case "oneway":
                    return new OnewayJestaDelegate();
                case "roundtrip":
                    return new RoundTripJestaDelegate();
            }
            return null;
        }
    }
}
