using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    abstract class WebScraping
    {
        private IWebScrapingDelegate _delegate;
        private Parameters _parameters;

        public Parameters Parameters 
        { 
            get { return _parameters; }
            set
            {
                _parameters = value;
                _delegate = GetWebScrapingDelegate(_parameters.BookingType);
            }
        }

        public ReturnData ParseResult(string fileName)
        {
            return _delegate.ParseResult(Parameters, fileName);
        }

        public void DownloadWebPage(String fileName)
        {
            _delegate.DownloadWebPage(Parameters, fileName);
        }

        public abstract IWebScrapingDelegate GetWebScrapingDelegate(string bookingType);
    }
}
