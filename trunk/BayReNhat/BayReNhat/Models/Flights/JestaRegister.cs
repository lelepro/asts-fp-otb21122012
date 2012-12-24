using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
namespace Flights
{

    class JestaRegister : FlightRegister
    {
        protected string _totalPrice = null;
        protected string _bookingType = null;
        protected string _market1 = null;
        protected string _market2 = null;

        void GetPostData(string fileName, FlightRegisterParameters param)
        {
            HtmlDocument document = new HtmlDocument(); ;
            document.Load(fileName);

            HtmlNode bookingTypeNode = document.DocumentNode.SelectSingleNode("//input[@id='marketstructure']");
            if (null == bookingTypeNode)
            {
                return;
            }
            _bookingType = bookingTypeNode.GetAttributeValue("value", "");

            HtmlNode totalPriceNode = document.DocumentNode.SelectSingleNode("//input[@id='total_price']");
            if (null == totalPriceNode)
            {
                return;
            }

            string totalPrice = totalPriceNode.GetAttributeValue("value", "");
            _totalPrice = totalPrice;

            _market1 = GetComboboxValue(document, param.DepartFlightNo, "1");
            _market2 = GetComboboxValue(document, param.ReturnFlightNo, "2");

        }

        protected void RoundTripResgister(FlightRegisterParameters param)
        {
            _totalPrice = "";
            _result = null;

            string fileName = Path.GetTempFileName();

            string cookie = "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; sessionculture=vi-VN; multitabtimestamp=1356139089393.86; pageRefreshStatus=freshPage; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=03/01/2013; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=checked; rdoFlightTypeOneWay#01=undefined; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356139295014; __utma=20450727.93390813.1356138950.1356138950.1356138950.1; __utmb=20450727.12.9.1356139295027; __utmc=20450727; __utmz=20450727.1356138950.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
            WebScraping webScraping = new JestaWebScraping();
            IWebScrapingDelegate webScrapingDelegate = webScraping.GetWebScrapingDelegate(param.BookingType);

            webScrapingDelegate.DownloadWebPage(param, fileName, cookie);
            GetPostData(fileName, param);

            string postData = string.Format(
                "__EVENTTARGET=ControlGroupSelectView%24LinkButtonSubmit&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgIFJ01lbWJlckxvZ2luU2VsZWN0VmlldyRtZW1iZXJfUmVtZW1iZXJtZQVEQ29udHJvbEdyb3VwU2VsZWN0VmlldyRBdmFpbGFiaWxpdHlJbnB1dFNlbGVjdFZpZXckUHJpY2VMb2NrQ2hlY2tib3jnzFtFjR2pAhdqCjacSoMMek2XLA%3D%3D&pageToken=&total_price={0}&ControlGroupSelectView%24AvailabilityInputSelectView%24HiddenFieldTabIndex1=4&ControlGroupSelectView%24AvailabilityInputSelectView%24market1={1}&ControlGroupSelectView%24AvailabilityInputSelectView%24HiddenFieldTabIndex2=4&ControlGroupSelectView%24AvailabilityInputSelectView%24market2={2}&marketstructure=RoundTrip", _totalPrice, _market1, _market2);

            WebClientEx webClient = WebClientEx.CreateJetstarWebClient("http://booknow.jetstar.com/Select.aspx");
            webClient.Headers["Cookie"] = "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=03/01/2013; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=checked; rdoFlightTypeOneWay#01=undefined; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356135533478; sessionculture=vi-VN; multitabtimestamp=1356135533320.23; __utma=20450727.1753434460.1356135509.1356135509.1356135509.1; __utmb=20450727.3.9.1356135533493; __utmc=20450727; __utmz=20450727.1356135509.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
            webClient.DownloadFileEx(postData, fileName);

            ParseResult(fileName);
            File.Delete(fileName);
        }

        protected  void OneWayRegister(FlightRegisterParameters param)
        {
            _result = null;

            string fileName = Path.GetTempFileName();

            WebScraping webScraping = new JestaWebScraping();
            IWebScrapingDelegate webScrapingDelegate = webScraping.GetWebScrapingDelegate(param.BookingType);

            string cookie =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; pageRefreshStatus=; sessionculture=vi-VN; multitabtimestamp=1356144700355.61; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=undefined; rdoFlightTypeOneWay#01=checked; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356145146686; __utma=20450727.93390813.1356138950.1356138950.1356141839.2; __utmb=20450727.17.9.1356145146700; __utmc=20450727; __utmz=20450727.1356141839.2.2.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
            webScrapingDelegate.DownloadWebPage(param, fileName, cookie);
            GetPostData(fileName, param);

            string postData = string.Format("__EVENTTARGET=ControlGroupSelectView%24LinkButtonSubmit&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgIFJ01lbWJlckxvZ2luU2VsZWN0VmlldyRtZW1iZXJfUmVtZW1iZXJtZQVEQ29udHJvbEdyb3VwU2VsZWN0VmlldyRBdmFpbGFiaWxpdHlJbnB1dFNlbGVjdFZpZXckUHJpY2VMb2NrQ2hlY2tib3jnzFtFjR2pAhdqCjacSoMMek2XLA%3D%3D&pageToken=&total_price{1}=&ControlGroupSelectView%24AvailabilityInputSelectView%24HiddenFieldTabIndex1=4&ControlGroupSelectView%24AvailabilityInputSelectView%24market1={0}&marketstructure=OneWay", _market1, _totalPrice);
            WebClientEx webClient = WebClientEx.CreateJetstarWebClient("http://booknow.jetstar.com/Select.aspx");
            webClient.Headers["Cookie"] =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; pageRefreshStatus=; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=undefined; rdoFlightTypeOneWay#01=checked; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356145146686; sessionculture=vi-VN; multitabtimestamp=1356145139199.53; __utma=20450727.93390813.1356138950.1356138950.1356141839.2; __utmb=20450727.18.9.1356145146700; __utmc=20450727; __utmz=20450727.1356141839.2.2.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
            webClient.DownloadFileEx(postData, fileName);

            ParseResult(fileName);
            File.Delete(fileName);
        }

        public override void Register(FlightRegisterParameters param)
        {
            if (param.BookingType == "oneway")
            {
                OneWayRegister(param);
            }
            else if (param.BookingType == "roundtrip")
            {
                RoundTripResgister(param);
            }
        }

        private string GetComboboxValue(HtmlDocument document, string flightNo, string comboboxPosition)
        {
            string result = "";

            string flightNoNumber = Regex.Replace(flightNo, "[^0-9]", "");
            string comboboxControlName = string.Format("ControlGroupSelectView$AvailabilityInputSelectView$market{0}", comboboxPosition);

            HtmlNodeCollection trNodes = document.DocumentNode.SelectNodes("//tr");
            if (null == trNodes)
            {
                return result;
            }

            foreach (var trNode in trNodes)
            {
                HtmlNode flightNoNode = trNode.SelectSingleNode(".//span[@class='flight-no']");
                if (flightNoNode != null)
                {
                    HtmlNode imgNode = flightNoNode.SelectSingleNode("./img");
                    if (imgNode != null)
                    {
                        flightNoNode.RemoveChild(imgNode);
                        if (flightNoNode.InnerText.Contains(flightNoNumber))
                        {
                            string xpath = string.Format(".//input[@name='{0}']", comboboxControlName);

                            HtmlNode radionButtonNode = trNode.SelectSingleNode(xpath);
                            if (radionButtonNode != null)
                            {
                                result = radionButtonNode.GetAttributeValue("value", "");
                                return result;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private void ParseResult(string fileName)
        {
            try
            {
                HtmlDocument document = new HtmlDocument();

                document.Load(fileName, System.Text.Encoding.UTF8);

                HtmlNode summaryNode = document.DocumentNode.SelectSingleNode("//div[@class='pass-summary']");
                if (null == summaryNode)
                {
                    return;
                }

    
                HtmlNodeCollection feeBeforeTaxNodes = summaryNode.SelectNodes(".//tr[@class='fares-người lớn']/td");
                if (null == feeBeforeTaxNodes)
                {
                    return;
                }

                int feeBeforeTaxNumber = 0;
                foreach (var feeBeforeTaxNode in feeBeforeTaxNodes)
                {
                    string feeBeforeTax = Utilities.JestaExtractNumber(feeBeforeTaxNode.InnerText);

                    try
                    {
                        feeBeforeTaxNumber += Convert.ToInt32(feeBeforeTax);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }

                int taxNumber = 0;

                HtmlNodeCollection feeItemHeaderNodes = summaryNode.SelectNodes(".//th[@class='fee-item']");
                foreach (var feeItemHeaderNode in feeItemHeaderNodes)
                {
                    HtmlNode taxNode = feeItemHeaderNode.SelectSingleNode("..//span");
                    if (null != taxNode)
                    {
                        string tax = Utilities.ExtractNumber(taxNode.InnerText);
                        try
                        {
                            taxNumber += Convert.ToInt32(tax);
                        }
                        catch (Exception)
                        {
                            return;
                        }
                    }
                }

                try
                {
                    _result = new RegisterResult();
                    _result.FeeBeforeTax = feeBeforeTaxNumber.ToString();
                    _result.Tax = taxNumber.ToString();

                    string bookId = Path.GetRandomFileName();
                    bookId = bookId.Replace(".", "");
                    _result.BookId = bookId;

                    _result.TotalFee = (feeBeforeTaxNumber + taxNumber).ToString();
                }
                catch (Exception)
                {
                    _result = null;
                }

            }
            catch (Exception)
            {

            }
        }
    }
}
