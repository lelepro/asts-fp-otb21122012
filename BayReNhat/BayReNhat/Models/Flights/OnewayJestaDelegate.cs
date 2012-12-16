using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Flights
{
    class OnewayJestaDelegate : JestaDelegate, IWebScrapingDelegate
    {
        public void DownloadWebPage(Parameters param, string fileName)
        {
            string postData =
                String.Format(
                    "ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListCurrency=VND&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListFareTypes=I&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListMarketDay1={2}&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListMarketDay2=1&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListMarketDay3=&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListMarketMonth1={4}-{3}&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListMarketMonth2=1968-1&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListMarketMonth3=&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListPassengerType_ADT=1&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListPassengerType_CHD=0&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24DropDownListPassengerType_INFANT=0&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24RadioButtonMarketStructure=OneWay&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24TextBoxMarketDestination1={1}&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24TextBoxMarketDestination2=&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24TextBoxMarketDestination3=&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24TextBoxMarketOrigin1={0}&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24TextBoxMarketOrigin2=&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24TextBoxMarketOrigin3=&ControlGroupSearchView%24ButtonSubmit=&__VIEWSTATE=%2FwEPDwUBMGRkuk23y%2Fc8bQS%2BtyzOftjdNwPRLxU%3D&culture=vi-VN&date_picker=&go-booking=&pageToken=sLkmnwXwAsY%3D&ControlGroupSearchView%24AvailabilitySearchInputSearchView%24fromCS=yes",
                    param.From, param.To, param.DepartDay, param.DepartMonth, param.DepartYear);

            WebClientEx web = WebClientEx.CreateJetstarWebClient(_host);
            web.DownloadFileEx(postData, fileName);
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
