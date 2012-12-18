using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    interface IWebScrapingDelegate
    {
        void DownloadWebPage(Parameters param, string fileName);
        void DownloadWebPage(Parameters param, string fileName, string cookie);
        ReturnData ParseResult(Parameters param, string fileName); 
    }
}
