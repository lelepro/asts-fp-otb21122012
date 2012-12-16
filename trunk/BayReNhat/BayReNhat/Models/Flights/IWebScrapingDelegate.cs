using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    interface IWebScrapingDelegate
    {
        void DownloadWebPage(Parameters param, string fileName);
        ReturnData ParseResult(Parameters param, string fileName); 
    }
}
