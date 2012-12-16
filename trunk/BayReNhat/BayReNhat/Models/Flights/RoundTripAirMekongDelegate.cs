using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Flights
{
    class RoundTripAirMekongDelegate : AirMekongDelegate, IWebScrapingDelegate
    {
        public void DownloadWebPage(Parameters param, string fileName)
        {
            string postData =
                String.Format(
                    "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGRk77h9CNekodLMZhdJu3riPx9ebN8%3D&pageToken=&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24RadioButtonMarketStructure=RoundTrip&originStation1={0}&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketOrigin1={0}&destinationStation1={1}&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketDestination1={1}&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDay1={2}&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketMonth1={4}-{3}&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDateRange1=0%7C4&originStation2=&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketOrigin2=%C4%90i%E1%BB%83m+%C4%91i...&destinationStation2=&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketDestination2=%C4%90i%E1%BB%83m+%C4%91%E1%BA%BFn...&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDay2={5}&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketMonth2={7}-{6}&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDateRange2=0%7C4&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListPassengerType_ADT=1&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListPassengerType_CHD=0&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListPassengerType_INFANT=0&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListSearchBy=columnView&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24ButtonSubmit=T%C3%ACm+chuy%E1%BA%BFn+bay",
                    param.From, param.To, param.DepartDay, param.DepartMonth, param.DepartYear,param.ReturnDay, param.ReturnMonth, param.ReturnYear);

            WebClientEx web = WebClientEx.CreateMekongAirWebClient(_host);
            web.DownloadFileEx(postData, fileName);
        }

        public ReturnData ParseResult(Parameters param, string fileName)
        {
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


            List<Flight> flights = ParseFlightTable(document, "availabilityTable1");
            returnTrip.Flights = flights;
            return returnTrip;

        }
    }
}
