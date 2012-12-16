﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace Flights
{
    class RoundTripVNAirlineDelegate : VNAirlineDelegate, IWebScrapingDelegate
    {
        public void DownloadWebPage(Parameters param, string fileName)
        {
            Normalize(param);
            String url = String.Format("https://cat.sabresonicweb.com/SSWVN/meridia?language=vi&posid=B3QE&page=requestAirMessage_air&action=airRequest&realrequestAir=realRequestAir&actionType=nonFlex&classService=CoachClass&currency=VND&depTime=0600&retTime=0600&direction=returntravel&departCity={0}&depDay={1}&depMonth={2}&temp_date=&returnCity={3}&retDay={4}&retMonth={5}&temp_date=&ADT=1&CHD=0&INF=0&wpf_timed_action_0VNAirline_00215IBEFastTabV2_00215bookingYourTrip_00515vn_0051513b9e830a94_0051570404_1=&wclang=VI&txtFamilyName=&txtMidName=&slDeparture=&txtConfirmCode=&txtFlightNumber=&rdoDay=on&", param.From, param.DepartDay, param.DepartMonth, param.To, param.ReturnDay, param.ReturnMonth);
            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, fileName);
        }

        public ReturnData ParseResult(Parameters param, string fileName)
        {
            //duplicate code. improve later

            ReturnData returnData = ReturnData.CreateReturnData(param.BookingType);

            HtmlDocument document = new HtmlDocument();
            document.Load(fileName);

            Trip departTrip = CreateDepartTrip(document, param);
            returnData.Data.Add(departTrip);

            Trip returnTrip = CreateReturnTrip(document, param);
            returnData.Data.Add(returnTrip);

            return returnData;
        }

        protected Trip CreateReturnTrip(HtmlDocument document, Parameters param)
        {
            Trip returnTrip = new Trip();
            returnTrip.DepartCity = param.To;
            returnTrip.ReturnCity = param.From;


            List<Flight> flights = ParseFlightTable(document, "bfm_tbl_in");
            returnTrip.Flights = flights;
            return returnTrip;

        }
    }
}
