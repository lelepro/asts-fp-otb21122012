using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Web;

namespace Flights
{
    class VNAirlineRegister : FlightRegister
    {
        public override void Register(FlightRegisterParameters param)
        {

            WebScraping webScraping = new VnAirlineWebScraping();
            IWebScrapingDelegate webScrapingDelegate = webScraping.GetWebScrapingDelegate(param.BookingType);

            string cookie = "BIGipServerprod_2.sabresonicweb_pool_4443=2231161098.23313.0000; BIGipServerprod.sabresonicweb_pool_4443=2801455114.11033.0000; __utma=60035391.1904160844.1355161353.1355762213.1355768273.23; __utmb=60035391.24.10.1355768273; __utmc=60035391; __utmz=60035391.1355768273.23.17.utmcsr=vietnamairlines.com|utmccn=(referral)|utmcmd=referral|utmcct=/wps/portal/vn/site/home";
            string fileName = Path.GetTempFileName();
            webScrapingDelegate.DownloadWebPage(param, fileName, cookie);

            HtmlDocument document = new HtmlDocument();
            document.Load(fileName);

            string continueUrl = GetContinueUrl(document, fileName);
        
            if (continueUrl == "")
            {
                return;
            }
            
            string outFormData = GetSelectedCombobox(document, param.DepartFlightNo, "bfm_tbl_out");
            string inFormData = GetSelectedCombobox(document, param.ReturnFlightNo, "bfm_tbl_in");

            param.BookingType = (param.BookingType == "roundtrip") ? "returntravel" : "onewaytravel";

            string postData = string.Format(
                "page=nonFlexairRequestMessage_air&action=airItinSelection&actionType=nonFlex&actionPage=&posid=B3QE&language=vi&direction={0}&departCity={1}&depDay={2}&depMonth={3}&depTime=0600&departTime=0600&returnCity={4}&retDay={5}&retMonth={6}&retTime=0600&returnTime=0600&ADT=1&ADT_lnform=1&OUT_FLIGHTS_SORTING_BY_DATE_TYPE=&IN_FLIGHTS_SORTING_BY_DATE_TYPE=&SHOW_FILTER_RANGE_FLIGHT_WARNING=&SHOW_FILTER_RANGE_OUTBOUND_FLIGHT_WARNING=&SHOW_FILTER_RANGE_INBOUND_FLIGHT_WARNING=&MATRIX_INTELLISELL=true&fareMatrixPrevURL=&out={7}&in={8}",
                param.BookingType, param.From, param.DepartDay, param.DepartMonth, param.To, param.ReturnDay, param.ReturnMonth, outFormData, inFormData);

            WebClientEx webClientEx = WebClientEx.CreateVNAirlineWebClient(continueUrl);
            webClientEx.DownloadFileEx(postData, fileName);

            //Get info
            document.Load(fileName);
            string totalFee = GetFee(document, "//span[@id='totalFee']");
            string feeBeforeTax = GetFee(document, "//p[@id='itinRevFare']");

            string url = string.Format("{0}&page=ssw_PAX_Info&action=SSWPAXInfo&hold_purchase=PURCHASE&shoppingCartUniqueIdForAir=shopping", continueUrl);
            webClientEx = WebClientEx.CreateVNAirlineWebClient(url);
            webClientEx.DownloadFileEx(postData, fileName);

            postData =
                "page=ssw_UserLoginMessage&posid=B3QE&shoppingCartUniqueIdForAir=shoppingCartUniqueIdForAir1&action=Guest&continueAsGuest=1&guest=true&reminderType=&language=vi&accountID=&password=&rememberLoginInfo=&hold_purchase=PURCHASE";
            webClientEx = WebClientEx.CreateVNAirlineWebClient(continueUrl);
            webClientEx.DownloadFileEx(postData, fileName);

            postData = string.Format("page=ssw_Payment_Info&shoppingCartUniqueIdForAir=shoppingCartUniqueIdForAir1&language=vi&partysize=1&firstTitle1=MR&paxType1=ADT&paxTypeDesc1=Ng%C6%B0%E1%BB%9Di+l%E1%BB%9Bn&firstName1={0}&lastName1={1}&freqflyer1=VN&frequent_flyer_num1=&ADTDay1=&ADTMonth1=&ADTYear1=&docType1=&docIssuedCountry1=&docNumber1=&docNationality1=&docExpirationDay1=&docExpirationMonth1=&docExpirationYear1=&docGender1=&hidDocPax1=false&docDayOfBirth1=&docMonthOfBirth1=&docYearOfBirth1=&mobile_PhoneCtryCode=84&mobile_PhoneCityCode=08&mobile_PhoneNumber={2}&evening_PhoneCtryCode=84&evening_PhoneCityCode=08&evening_PhoneNumber={3}&day_PhoneCtryCode=&day_PhoneCityCode=&day_PhoneNumber=&User_Email1={4}&EMAILFLAG=TRUE&Verify_User_Email1={5}&fax_PhoneCtryCode=&fax_PhoneCityCode=&fax_PhoneNumber=&User_Email2=&Verify_User_Email2=&EMAILFLAG2=TRUE&aeClicked=false&action=SSWPaymentInfo",
                param.FirstName, param.LastName, param.Phone, param.Phone, param.Email, param.Email);
            webClientEx.DownloadFileEx(postData, fileName);

            postData =
                string.Format(
                    "page=ssw_Payment_Info&TDS_PHASE=Validate&isCustomCCard=false&shoppingCartUniqueIdForAir=shoppingCartUniqueIdForAir1&language=vi&TOTAL_DUE={0}&TOTAL_CC_DUE={0}&SEAT_TOTAL=0&SSR_TOTAL=0.0&TOTAL_AIR_TAXES={0}&CHANGE_FEE=0&OTHER_FEE=0&REFUND_AMT=0&FEE_TAX=0&SEC_FEE_TAX=0&EXCH_CREDIT=0&serverUrl=https%3A%2F%2Fcat.sabresonicweb.com&TOTAL_CREDIT_BANK=0&TOTAL_BEFORE_CC_SURCHARGE={1}.00&TOTAL_BEFORE_INSURANCE={0}.00&TOTAL_AFTER_CC_SURCHARGE={0}.00&COMMA_FORMATTING=true&displayPinCC=&ccCode=XX&ccCodeSelectedIndex=&cardNumber=&cardValidFromMonth=&cardValidFromYear=&cardHolderName=&cardExpMonth=1&cardExpYear=2012&cardHolderSecurityCode=&cardHolderPinCode=&cardIssueNumber=&payHow=AGENCY&agCode=VCB+ATM+machines&billingAddress1=&billingAddress2=&billingCity=&billingStateProv=&billingZip=&billingCountry=VN&deliverHow=eticket&deliveryAddress1=&deliveryCity=&deliveryAddress2=&deliveryAddress3=&deliveryAddress4=&deliveryStateProv=&deliveryZip=&deliveryCountry&termsConditions=on&action=finalReview&purch_submit=Mua",
                    totalFee, feeBeforeTax);

            webClientEx.DownloadFileEx(postData, fileName);
            document.Load(fileName);
            string bookId = GetBookId(document);

            if (bookId != null && bookId.Trim().Length > 0)
            {
                _result = new RegisterResult();

                _result.BookId = bookId;
                _result.TotalFee = totalFee;
                _result.FeeBeforeTax = feeBeforeTax;

                try
                {
                    _result.Tax = (int.Parse(totalFee) - int.Parse(feeBeforeTax)).ToString();
                }
                catch (Exception)
                {
                    _result = null;
                }
            }
            File.Delete(fileName);
        }

        private string GetBookId(HtmlDocument document)
        {
            HtmlNode bookIdNode = document.DocumentNode.SelectSingleNode("//input[@id='confirmationNumber']");

            if (null == bookIdNode)
            {
                return "";
            }

            string bookId = bookIdNode.GetAttributeValue("value", "");
            return bookId;
        }

        private string GetFee(HtmlDocument document, string xpath)
        {
            HtmlNode totalFee = document.DocumentNode.SelectSingleNode(xpath);
            if (totalFee == null)
            {
                return "";
            }
            string totalFeeNumber = Regex.Replace(totalFee.InnerText, "[^0-9]", "");

            return totalFeeNumber;
        }


        private string GetSelectedCombobox(HtmlDocument document, string flightNo, string tableId)
        {
            string flightNoNumberOnly = Regex.Match(flightNo, @"\d+").Value;

            string xpath = String.Format("//table[@id='{0}']/tbody/tr", tableId);

            HtmlNodeCollection rowCollection = document.DocumentNode.SelectNodes(xpath);
            if (null == rowCollection)
            {
                return "";
            }

            foreach (HtmlNode row in rowCollection)
            {
                HtmlNode node = row.SelectSingleNode(".//span[@class='matrix_airline_code']");
                if (node != null && node.InnerText.Contains(flightNoNumberOnly) == true)
                {
                    HtmlNodeCollection priceColletion = row.SelectNodes(".//span[@class='step2PricePoints ']//a");
                    if (null == priceColletion)
                    {
                        break;
                    }

                    HtmlNode cheapestPriceNode = priceColletion.Last();
                    HtmlNode comboboxNode = cheapestPriceNode.SelectSingleNode("../../input");
                    if (comboboxNode != null)
                    {
                        string value = comboboxNode.GetAttributeValue("value", "");
                        return value;
                    }

                }
            }

            return "";
        }

        private string GetContinueUrl(HtmlDocument document, string fileName)
        {
   
            HtmlNode node = document.DocumentNode.SelectSingleNode("//form[@name='airAvailNonForm']");
            string url = "";

            if (node != null)
            {
                url = node.GetAttributeValue("action", "");
                url = url.Replace("9yyv", "yyyv");
                url = string.Format("https://cat.sabresonicweb.com{0}", url);
            }
            
            return url;
        }
    }
}
