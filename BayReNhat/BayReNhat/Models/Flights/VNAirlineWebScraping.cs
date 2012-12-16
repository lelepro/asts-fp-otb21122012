using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;

namespace Flights
{
    class VnAirlineWebScraping : WebScraping
    {
        public override IWebScrapingDelegate GetWebScrapingDelegate(string bookingType)
        {
            switch (bookingType)
            {
                case "oneway":
                    return new OnewayVNAirlineDelegate();
                case "roundtrip":
                    return new RoundTripVNAirlineDelegate();
            }
            return null;
        }
    }
}
