using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace Flights
{
    class OnewayVNAirlineDelegate : VNAirlineDelegate, IWebScrapingDelegate
    {
        public void DownloadWebPage(Parameters param, string fileName)
        {
            Normalize(param);
            String url = String.Format("https://cat.sabresonicweb.com/SSWVN/meridia?language=vi&posid=B3QE&page=requestAirMessage_air&action=airRequest&realrequestAir=realRequestAir&actionType=nonFlex&classService=CoachClass&currency=VND&depTime=0600&retTime=0600&direction=onewaytravel&departCity={0}&depDay={1}&depMonth={2}&temp_date=&returnCity={3}&temp_date=&ADT=1&CHD=0&INF=0&wpf_timed_action_0VNAirline_00215IBEFastTabV2_00215bookingYourTrip_00515vn_0051513b9a929b93_0051562f20_1=&&wclang=VI&txtFamilyName=&txtMidName=&slDeparture=&txtConfirmCode=&txtFlightNumber=&rdoDay=on&", param.From, param.DepartDay, param.DepartMonth, param.To);
            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, fileName);
        }

        public ReturnData ParseResult(Parameters param, string fileName)
        {
            ReturnData returnData = ReturnData.CreateReturnData(param.BookingType);

            HtmlDocument document = new HtmlDocument();
            document.Load(fileName);

            Trip departTrip = CreateDepartTrip(document, param);
            returnData.Data.Add(departTrip);

            return returnData;
        }
    }
}
