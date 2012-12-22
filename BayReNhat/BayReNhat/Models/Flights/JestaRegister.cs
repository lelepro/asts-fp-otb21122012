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


            webClient = WebClientEx.CreateJetstarWebClient("https://booknow.jetstar.com/Passenger.aspx");
            webClient.Headers["Cookie"] =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; pageRefreshStatus=freshPage; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=03/01/2013; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=checked; rdoFlightTypeOneWay#01=undefined; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356139295014; sessionculture=vi-VN; multitabtimestamp=1356139308504.07; __utma=20450727.93390813.1356138950.1356138950.1356138950.1; __utmb=20450727.14.9.1356139295027; __utmc=20450727; __utmz=20450727.1356138950.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
            postData = string.Format(
                "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFg0FLE1lbWJlckRpc3BsYXlQYXNzZW5nZXJWaWV3JG1lbWJlcl9SZW1lbWJlcm1lBZUBQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRQYXNzZW5nZXJJbnB1dFZpZXdQYXNzZW5nZXJWaWV3JFNwZWNpYWxBc3Npc3RhbmNlQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRXaGVlbENoYWlyU3NySW5wdXRQYXNzZW5nZXJWaWV3JFF1ZXN0aW9uQ2hlY2tCb3gFtQFDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFBhc3NlbmdlcklucHV0Vmlld1Bhc3NlbmdlclZpZXckU3BlY2lhbEFzc2lzdGFuY2VDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFdoZWVsQ2hhaXJTc3JJbnB1dFBhc3NlbmdlclZpZXckV2hlZWxDaGFpclNzcklucHV0UGFzc2VuZ2VyVmlld19DaGVja0JveF9TdGFmZl8wBY8BQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRQYXNzZW5nZXJJbnB1dFZpZXdQYXNzZW5nZXJWaWV3JFNwZWNpYWxBc3Npc3RhbmNlQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRTQVVwcGVyVG9yc29QYXNzZW5nZXJWaWV3JFF1ZXN0aW9uQ2hlY2tCb3gFkwFDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFBhc3NlbmdlcklucHV0Vmlld1Bhc3NlbmdlclZpZXckU3BlY2lhbEFzc2lzdGFuY2VDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFNBVmlzaW9uSW1wYWlyZWRQYXNzZW5nZXJWaWV3JFF1ZXN0aW9uQ2hlY2tCb3gFlAFDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFBhc3NlbmdlcklucHV0Vmlld1Bhc3NlbmdlclZpZXckU3BlY2lhbEFzc2lzdGFuY2VDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFNBSGVhcmluZ0ltcGFpcmVkUGFzc2VuZ2VyVmlldyRRdWVzdGlvbkNoZWNrQm94BZEBQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRQYXNzZW5nZXJJbnB1dFZpZXdQYXNzZW5nZXJWaWV3JFNwZWNpYWxBc3Npc3RhbmNlQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRTQVRyYXZlbE94eWdlblBhc3NlbmdlclZpZXckUXVlc3Rpb25DaGVja0JveAWVAUNvbnRyb2xHcm91cFBhc3NlbmdlclZpZXckUGFzc2VuZ2VySW5wdXRWaWV3UGFzc2VuZ2VyVmlldyRTcGVjaWFsQXNzaXN0YW5jZUNvbnRyb2xHcm91cFBhc3NlbmdlclZpZXckU0FNZWRpY2FsQ2xlYXJhbmNlUGFzc2VuZ2VyVmlldyRRdWVzdGlvbkNoZWNrQm94BUpDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFBhc3NlbmdlcklucHV0Vmlld1Bhc3NlbmdlclZpZXckU01TSXRpbkNoZWNrQm94MAVkQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRDb250YWN0SW5wdXRWaWV3UGFzc2VuZ2VyVmlldyRTTVNJdGluZXJhcnlDb250YWN0VmlldyRDaGVja0JveFNNU0l0aW5lcmFyeQVHQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRDb250YWN0SW5wdXRWaWV3UGFzc2VuZ2VyVmlldyRDaGVja0JveEpldE1haWwFRkNvbnRyb2xHcm91cFBhc3NlbmdlclZpZXckQ29udGFjdElucHV0Vmlld1Bhc3NlbmdlclZpZXckQ2hlY2tCb3hKZXRUeHQFSENvbnRyb2xHcm91cFBhc3NlbmdlclZpZXckQ29udGFjdElucHV0Vmlld1Bhc3NlbmdlclZpZXckQ2hlY2tCb3hKZXRDb21iaWh4J9%2FESyAhhfCpUGFatcsb1jVl&pageToken=&total_price=120%2C43&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListTitle_1=MR&ControlGroupPassengerView%24PassengerInputViewPassengerView%24TextBoxFirstName_1={0}&default-value-firstname-1=T%C3%AAn&ControlGroupPassengerView%24PassengerInputViewPassengerView%24TextBoxLastName_1={1}&default-value-lastname-1=H%E1%BB%8D&ControlGroupPassengerView%24PassengerInputViewPassengerView%24TextBoxProgramNumber_1=&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListProgram_1=QF&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListGender_1=1&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListBirthDateDay_1=11&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListBirthDateMonth_1=11&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListBirthDateYear_1=2001&ControlGroupPassengerView%24PassengerInputViewPassengerView%24AdditionalBaggagePassengerView%24AdditionalBaggageGlobalDropDownList=BG20&ControlGroupPassengerView%24PassengerInputViewPassengerView%24AdditionalBaggagePassengerView%24AdditionalBaggageDropDownListJourney0Pax0=BG20&ControlGroupPassengerView%24PassengerInputViewPassengerView%24AdditionalBaggagePassengerView%24AdditionalBaggageDropDownListJourney1Pax0=BG20&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListTitle=MR&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxFirstName={0}&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxLastName={1}&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxEmailAddress={3}&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxEmailAddressConfirm={3}&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListCountryCode=VN&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxOtherPhone={2}&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListHomePhoneCountryCode=VN&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxHomePhone={2}&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListWorkPhoneCountryCode=VN&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxWorkPhone={2}&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxAddressLine1=ttt&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxAddressLine2=&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxCity=ttt&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListCountry=VN&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListStateProvince=VN%7CHAN&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxPostalCode=84&ControlGroupPassengerView%24ItineraryDistributionInputPassengerView%24Distribution=2&ControlGroupPassengerView%24ButtonSubmit=Continue",
                param.FirstName, param.LastName, param.Phone, param.Email);

            webClient.DownloadFileEx(postData, fileName);

            webClient = WebClientEx.CreateJetstarWebClient("https://booknow.jetstar.com/Seats.aspx");
            webClient.Headers["Cookie"] =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; pageRefreshStatus=freshPage; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=03/01/2013; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=checked; rdoFlightTypeOneWay#01=undefined; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356139295014; sessionculture=vi-VN; multitabtimestamp=1356139375711.83; __utma=20450727.93390813.1356138950.1356138950.1356138950.1; __utmb=20450727.16.9.1356139392582; __utmc=20450727; __utmz=20450727.1356138950.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";

            postData = string.Format("__EVENTTARGET=ControlGroupSeatsView%24UnitMapSeatsView%24LinkButtonCancelAllPaxSeats&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGRkuk23y%2Fc8bQS%2BtyzOftjdNwPRLxU%3D&pageToken=&total_price={0}&ControlGroupSeatsView%24UnitMapSeatsView%24HiddenEquipmentConfiguration_0_PassengerNumber_0=&ControlGroupSeatsView%24UnitMapSeatsView%24HiddenEquipmentConfiguration_1_PassengerNumber_0=",
                _totalPrice);
            webClient.DownloadFileEx(postData, fileName);

            webClient = WebClientEx.CreateJetstarWebClient("https://booknow.jetstar.com/Addons.aspx");

            postData = string.Format(
                "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgUFJ01lbWJlckxvZ2luQWRkb25zVmlldyRtZW1iZXJfUmVtZW1iZXJtZQVCQ29udHJvbEdyb3VwQWRkb25zVmlldyRJbnN1cmFuY2VBdmFpbGFiaWxpdHlBZGRvbnNWaWV3JGpldGNvdmVyWWVzBUJDb250cm9sR3JvdXBBZGRvbnNWaWV3JEluc3VyYW5jZUF2YWlsYWJpbGl0eUFkZG9uc1ZpZXckamV0Y292ZXJZZXMFQUNvbnRyb2xHcm91cEFkZG9uc1ZpZXckSW5zdXJhbmNlQXZhaWxhYmlsaXR5QWRkb25zVmlldyRqZXRjb3Zlck5vBUFDb250cm9sR3JvdXBBZGRvbnNWaWV3JEluc3VyYW5jZUF2YWlsYWJpbGl0eUFkZG9uc1ZpZXckamV0Y292ZXJOb%2FDLJAtLMwFaK%2B4CsanMZedviDOj&pageToken=&total_price={0}&InsuranceItem%24SlF-fmVuLUFVfn5WTn5-Vk5Efn4wMjEwMzF-fklOU35-QkxHT0xEfn5-fjYzNDkxNjA2NzM1NDUyNzIyOX5-NH5-U0dOfn42MzQ5MTgzOTIwMDAwMDAwMDB-fkhBTn5-NjM0OTE4NDcwMDAwMDAwMDAwfn5IQU5-fjYzNDkyMDIxOTAwMDAwMDAwMH5-U0dOfn42MzQ5MjAyOTQwMDAwMDAwMDB-fjF-fjYzMzc0MTQwODAwMDAwMDAwMH5-QURUfn5UcnVlfn4wfn4%3A=MX5-R09MRH5-Vk5-fjF-fn5-fn4wfn5KU1RiNmY3ZjcyZTcwYX5-U1NMfn5CeVNrdURldGFpbHxCZXR3ZWVufEJvdGh8Qm90aHxlbi1BVXxKUXxWTkR8Vk58Vk58QkxHT0xEfHwwfC0xfElOU3x8fHwwfDB8fDF8MTIvMjAvMjAxMiAxOjI1OjM1IFBNfEZhbHNlfFRydWV8RmFsc2V8VHJ1ZXx8KHx8U0dOfDEyLzIzLzIwMTIgNjowMDowMCBBTSkofHxIQU58MTIvMjMvMjAxMiA4OjEwOjAwIEFNKSh8fEhBTnwxMi8yNS8yMDEyIDg6NDU6MDAgQU0pKHx8U0dOfDEyLzI1LzIwMTIgMTA6NTA6MDAgQU0pfCh8fDQvMS8yMDA5IDEyOjAwOjAwIEFNfEFEVHxUcnVlfDApfGFub255bW91c3x8fHxBY3RpdmV8VHJ1ZXxUcnVlfEluZmluaXRlfn5DUkx-fg%3A%3A&ControlGroupAddonsView%24InsuranceAvailabilityAddonsView%24jetcover=jetcoverNo&ControlGroupAddonsView%24ButtonSubmit=",
                _totalPrice);
            webClient.Headers["Cookie"] =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; pageRefreshStatus=freshPage; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=03/01/2013; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=checked; rdoFlightTypeOneWay#01=undefined; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356139295014; multitabtimestamp=1356139375711.83; __utma=20450727.93390813.1356138950.1356138950.1356138950.1; __utmb=20450727.16.9.1356139392582; __utmc=20450727; __utmz=20450727.1356138950.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); sessionculture=vi-VN";
            webClient.DownloadFileEx(postData, fileName);

            webClient = WebClientEx.CreateJetstarWebClient("https://booknow.jetstar.com/Pay.aspx");

            postData = string.Format(
                "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgEFPENvbnRyb2xHcm91cFBheVZpZXckU01TSXRpbmVyYXJ5UGF5VmlldyRDaGVja0JveFNNU0l0aW5lcmFyeZTFvnIzvPC0sz%2FO55AoTt2QBtCf&pageToken=&total_price={0}%2C89&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24PaymentMethodDropDown=PrePaid-BL&card_number1=&card_number2=&card_number3=&card_number4=&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24TextBoxCC__AccountHolderName=&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24DropDownListEXPDAT_Month=12&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24DropDownListEXPDAT_Year=2012&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24TextBoxCC__VerificationCode=&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24TextBoxACCTNO=&inlineDCCAjaxSucceeded=false&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24TextBoxVoucherAccount_VO_ACCTNO=&ControlGroupPayView%24AgreementInputPayView%24CheckBoxAgreement=on&ControlGroupPayView%24ButtonSubmit=",
                _totalPrice);
            webClient.Headers["Cookie"] =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=03/01/2013; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=checked; rdoFlightTypeOneWay#01=undefined; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356139295014; sessionculture=vi-VN; multitabtimestamp=1356139390514.54; pageRefreshStatus=freshPage; __utma=20450727.93390813.1356138950.1356138950.1356138950.1; __utmb=20450727.23.8.1356139411341; __utmc=20450727; __utmz=20450727.1356138950.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
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

            webClient = WebClientEx.CreateJetstarWebClient("https://booknow.jetstar.com/Passenger.aspx");
            webClient.Headers["Cookie"] =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; pageRefreshStatus=; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=undefined; rdoFlightTypeOneWay#01=checked; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356145146686; sessionculture=vi-VN; multitabtimestamp=1356145157607.81; __utma=20450727.93390813.1356138950.1356138950.1356141839.2; __utmb=20450727.19.9.1356145146700; __utmc=20450727; __utmz=20450727.1356141839.2.2.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
            postData = string.Format(
                "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFg0FLE1lbWJlckRpc3BsYXlQYXNzZW5nZXJWaWV3JG1lbWJlcl9SZW1lbWJlcm1lBZUBQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRQYXNzZW5nZXJJbnB1dFZpZXdQYXNzZW5nZXJWaWV3JFNwZWNpYWxBc3Npc3RhbmNlQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRXaGVlbENoYWlyU3NySW5wdXRQYXNzZW5nZXJWaWV3JFF1ZXN0aW9uQ2hlY2tCb3gFtQFDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFBhc3NlbmdlcklucHV0Vmlld1Bhc3NlbmdlclZpZXckU3BlY2lhbEFzc2lzdGFuY2VDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFdoZWVsQ2hhaXJTc3JJbnB1dFBhc3NlbmdlclZpZXckV2hlZWxDaGFpclNzcklucHV0UGFzc2VuZ2VyVmlld19DaGVja0JveF9TdGFmZl8wBY8BQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRQYXNzZW5nZXJJbnB1dFZpZXdQYXNzZW5nZXJWaWV3JFNwZWNpYWxBc3Npc3RhbmNlQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRTQVVwcGVyVG9yc29QYXNzZW5nZXJWaWV3JFF1ZXN0aW9uQ2hlY2tCb3gFkwFDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFBhc3NlbmdlcklucHV0Vmlld1Bhc3NlbmdlclZpZXckU3BlY2lhbEFzc2lzdGFuY2VDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFNBVmlzaW9uSW1wYWlyZWRQYXNzZW5nZXJWaWV3JFF1ZXN0aW9uQ2hlY2tCb3gFlAFDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFBhc3NlbmdlcklucHV0Vmlld1Bhc3NlbmdlclZpZXckU3BlY2lhbEFzc2lzdGFuY2VDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFNBSGVhcmluZ0ltcGFpcmVkUGFzc2VuZ2VyVmlldyRRdWVzdGlvbkNoZWNrQm94BZEBQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRQYXNzZW5nZXJJbnB1dFZpZXdQYXNzZW5nZXJWaWV3JFNwZWNpYWxBc3Npc3RhbmNlQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRTQVRyYXZlbE94eWdlblBhc3NlbmdlclZpZXckUXVlc3Rpb25DaGVja0JveAWVAUNvbnRyb2xHcm91cFBhc3NlbmdlclZpZXckUGFzc2VuZ2VySW5wdXRWaWV3UGFzc2VuZ2VyVmlldyRTcGVjaWFsQXNzaXN0YW5jZUNvbnRyb2xHcm91cFBhc3NlbmdlclZpZXckU0FNZWRpY2FsQ2xlYXJhbmNlUGFzc2VuZ2VyVmlldyRRdWVzdGlvbkNoZWNrQm94BUpDb250cm9sR3JvdXBQYXNzZW5nZXJWaWV3JFBhc3NlbmdlcklucHV0Vmlld1Bhc3NlbmdlclZpZXckU01TSXRpbkNoZWNrQm94MAVkQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRDb250YWN0SW5wdXRWaWV3UGFzc2VuZ2VyVmlldyRTTVNJdGluZXJhcnlDb250YWN0VmlldyRDaGVja0JveFNNU0l0aW5lcmFyeQVHQ29udHJvbEdyb3VwUGFzc2VuZ2VyVmlldyRDb250YWN0SW5wdXRWaWV3UGFzc2VuZ2VyVmlldyRDaGVja0JveEpldE1haWwFRkNvbnRyb2xHcm91cFBhc3NlbmdlclZpZXckQ29udGFjdElucHV0Vmlld1Bhc3NlbmdlclZpZXckQ2hlY2tCb3hKZXRUeHQFSENvbnRyb2xHcm91cFBhc3NlbmdlclZpZXckQ29udGFjdElucHV0Vmlld1Bhc3NlbmdlclZpZXckQ2hlY2tCb3hKZXRDb21iaWh4J9%2FESyAhhfCpUGFatcsb1jVl&pageToken=&total_price=120%2C43&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListTitle_1=MR&ControlGroupPassengerView%24PassengerInputViewPassengerView%24TextBoxFirstName_1={0}&default-value-firstname-1=T%C3%AAn&ControlGroupPassengerView%24PassengerInputViewPassengerView%24TextBoxLastName_1={1}&default-value-lastname-1=H%E1%BB%8D&ControlGroupPassengerView%24PassengerInputViewPassengerView%24TextBoxProgramNumber_1=&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListProgram_1=QF&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListGender_1=1&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListBirthDateDay_1=11&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListBirthDateMonth_1=11&ControlGroupPassengerView%24PassengerInputViewPassengerView%24DropDownListBirthDateYear_1=2001&ControlGroupPassengerView%24PassengerInputViewPassengerView%24AdditionalBaggagePassengerView%24AdditionalBaggageGlobalDropDownList=BG20&ControlGroupPassengerView%24PassengerInputViewPassengerView%24AdditionalBaggagePassengerView%24AdditionalBaggageDropDownListJourney0Pax0=BG20&ControlGroupPassengerView%24PassengerInputViewPassengerView%24AdditionalBaggagePassengerView%24AdditionalBaggageDropDownListJourney1Pax0=BG20&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListTitle=MR&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxFirstName={0}&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxLastName={1}&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxEmailAddress={3}&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxEmailAddressConfirm={3}&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListCountryCode=VN&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxOtherPhone={2}&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListHomePhoneCountryCode=VN&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxHomePhone={2}&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListWorkPhoneCountryCode=VN&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxWorkPhone={2}&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxAddressLine1=ttt&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxAddressLine2=&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxCity=ttt&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListCountry=VN&ControlGroupPassengerView%24ContactInputViewPassengerView%24DropDownListStateProvince=VN%7CHAN&ControlGroupPassengerView%24ContactInputViewPassengerView%24TextBoxPostalCode=84&ControlGroupPassengerView%24ItineraryDistributionInputPassengerView%24Distribution=2&ControlGroupPassengerView%24ButtonSubmit=Continue",
                param.FirstName, param.LastName, param.Phone, param.Email);
            webClient.DownloadFileEx(postData, fileName);

            webClient = WebClientEx.CreateJetstarWebClient("https://booknow.jetstar.com/Seats.aspx");
            webClient.Headers["Cookie"] =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; pageRefreshStatus=; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=undefined; rdoFlightTypeOneWay#01=checked; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356145146686; sessionculture=vi-VN; multitabtimestamp=1356145208933.76; __utma=20450727.93390813.1356138950.1356138950.1356141839.2; __utmb=20450727.21.9.1356145226628; __utmc=20450727; __utmz=20450727.1356141839.2.2.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
            postData = string.Format("__EVENTTARGET=ControlGroupSeatsView%24UnitMapSeatsView%24LinkButtonCancelAllPaxSeats&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGRkuk23y%2Fc8bQS%2BtyzOftjdNwPRLxU%3D&pageToken=&total_price={0}&ControlGroupSeatsView%24UnitMapSeatsView%24HiddenEquipmentConfiguration_0_PassengerNumber_0=&ControlGroupSeatsView%24UnitMapSeatsView%24HiddenEquipmentConfiguration_1_PassengerNumber_0=",
                _totalPrice);
            webClient.DownloadFileEx(postData, fileName);

            webClient = WebClientEx.CreateJetstarWebClient("https://booknow.jetstar.com/Addons.aspx");
            postData = string.Format(
                "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgUFJ01lbWJlckxvZ2luQWRkb25zVmlldyRtZW1iZXJfUmVtZW1iZXJtZQVCQ29udHJvbEdyb3VwQWRkb25zVmlldyRJbnN1cmFuY2VBdmFpbGFiaWxpdHlBZGRvbnNWaWV3JGpldGNvdmVyWWVzBUJDb250cm9sR3JvdXBBZGRvbnNWaWV3JEluc3VyYW5jZUF2YWlsYWJpbGl0eUFkZG9uc1ZpZXckamV0Y292ZXJZZXMFQUNvbnRyb2xHcm91cEFkZG9uc1ZpZXckSW5zdXJhbmNlQXZhaWxhYmlsaXR5QWRkb25zVmlldyRqZXRjb3Zlck5vBUFDb250cm9sR3JvdXBBZGRvbnNWaWV3JEluc3VyYW5jZUF2YWlsYWJpbGl0eUFkZG9uc1ZpZXckamV0Y292ZXJOb%2FDLJAtLMwFaK%2B4CsanMZedviDOj&pageToken=&total_price={0}&InsuranceItem%24SlF-fmVuLUFVfn5WTn5-Vk5Efn4wMjEwMzF-fklOU35-QkxHT0xEfn5-fjYzNDkxNjA2NzM1NDUyNzIyOX5-NH5-U0dOfn42MzQ5MTgzOTIwMDAwMDAwMDB-fkhBTn5-NjM0OTE4NDcwMDAwMDAwMDAwfn5IQU5-fjYzNDkyMDIxOTAwMDAwMDAwMH5-U0dOfn42MzQ5MjAyOTQwMDAwMDAwMDB-fjF-fjYzMzc0MTQwODAwMDAwMDAwMH5-QURUfn5UcnVlfn4wfn4%3A=MX5-R09MRH5-Vk5-fjF-fn5-fn4wfn5KU1RiNmY3ZjcyZTcwYX5-U1NMfn5CeVNrdURldGFpbHxCZXR3ZWVufEJvdGh8Qm90aHxlbi1BVXxKUXxWTkR8Vk58Vk58QkxHT0xEfHwwfC0xfElOU3x8fHwwfDB8fDF8MTIvMjAvMjAxMiAxOjI1OjM1IFBNfEZhbHNlfFRydWV8RmFsc2V8VHJ1ZXx8KHx8U0dOfDEyLzIzLzIwMTIgNjowMDowMCBBTSkofHxIQU58MTIvMjMvMjAxMiA4OjEwOjAwIEFNKSh8fEhBTnwxMi8yNS8yMDEyIDg6NDU6MDAgQU0pKHx8U0dOfDEyLzI1LzIwMTIgMTA6NTA6MDAgQU0pfCh8fDQvMS8yMDA5IDEyOjAwOjAwIEFNfEFEVHxUcnVlfDApfGFub255bW91c3x8fHxBY3RpdmV8VHJ1ZXxUcnVlfEluZmluaXRlfn5DUkx-fg%3A%3A&ControlGroupAddonsView%24InsuranceAvailabilityAddonsView%24jetcover=jetcoverNo&ControlGroupAddonsView%24ButtonSubmit=",
                _totalPrice);
            webClient.Headers["Cookie"] =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; pageRefreshStatus=; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=undefined; rdoFlightTypeOneWay#01=checked; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356145146686; sessionculture=vi-VN; multitabtimestamp=1356145217289.75; __utma=20450727.93390813.1356138950.1356138950.1356141839.2; __utmb=20450727.22.9.1356145226628; __utmc=20450727; __utmz=20450727.1356141839.2.2.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
            webClient.DownloadFileEx(postData, fileName);

            webClient = WebClientEx.CreateJetstarWebClient("https://booknow.jetstar.com/Pay.aspx");
            postData = string.Format(
                "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgEFPENvbnRyb2xHcm91cFBheVZpZXckU01TSXRpbmVyYXJ5UGF5VmlldyRDaGVja0JveFNNU0l0aW5lcmFyeZTFvnIzvPC0sz%2FO55AoTt2QBtCf&pageToken=&total_price={0}%2C89&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24PaymentMethodDropDown=PrePaid-BL&card_number1=&card_number2=&card_number3=&card_number4=&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24TextBoxCC__AccountHolderName=&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24DropDownListEXPDAT_Month=12&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24DropDownListEXPDAT_Year=2012&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24TextBoxCC__VerificationCode=&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24TextBoxACCTNO=&inlineDCCAjaxSucceeded=false&ControlGroupPayView%24PaymentSectionPayView%24UpdatePanelPayView%24PaymentInputPayView%24TextBoxVoucherAccount_VO_ACCTNO=&ControlGroupPayView%24AgreementInputPayView%24CheckBoxAgreement=on&ControlGroupPayView%24ButtonSubmit=",
                _totalPrice);
            webClient.Headers["Cookie"] =
                "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; multitabcounttimestamp=3; cae_browser=desktop; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtOriginCityCode#01=SGN; txtDestination#01=H%E0%20N%u1ED9i%20%28HAN%29; txtDestinationCityCode#01=HAN; txtDepart#01=30/12/2012; txtReturn#01=; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=undefined; rdoFlightTypeOneWay#01=checked; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1356145146686; sessionculture=vi-VN; multitabtimestamp=1356145226286.45; pageRefreshStatus=freshPage; __utma=20450727.93390813.1356138950.1356138950.1356141839.2; __utmb=20450727.28.9.1356145245526; __utmc=20450727; __utmz=20450727.1356141839.2.2.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
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

        private void ParseResult (string fileName)
        {
            try
            {
                HtmlDocument document = new HtmlDocument();

                document.Load(fileName, System.Text.Encoding.UTF8);

                HtmlNode bookIdNode = document.DocumentNode.SelectSingleNode("//div[@class='ref-number']/span");
                if (null == bookIdNode)
                {
                    return;
                }

                string bookId = bookIdNode.InnerText;
                HtmlNode paymentTableNode = document.DocumentNode.SelectSingleNode("//table[@class='payments']");
                
                if (null == paymentTableNode)
                {
                    return;
                }

                int feeBeforeTax = 0;

                HtmlNodeCollection paymentTabaleBody = paymentTableNode.SelectNodes(".//table");
                if (paymentTabaleBody != null && paymentTabaleBody.Count > 1)
                {
                    HtmlNode paymentDetail = paymentTabaleBody.First();
                    HtmlNodeCollection tdNodes = paymentDetail.SelectNodes(".//td[@class='col-a']");

                    if (tdNodes != null)
                    {
                        foreach (var tdNode in tdNodes)
                        {
                            if (tdNode.InnerText.Contains("Giá vé"))
                            {
                                HtmlNode price = tdNode.SelectSingleNode("../td[@class='col-b']");
                                if (price != null)
                                {
                                    string text = Regex.Replace(price.InnerText, "[^0-9]", "");
                                    try
                                    {
                                        feeBeforeTax += Convert.ToInt32(text);
                                    }
                                    catch (Exception)
                                    {
                                        feeBeforeTax = 0;
                                    }
                                
                                }
                            }
                        }
                    }
                }

                if (0 == feeBeforeTax)
                {
                    return;
                }

                HtmlNodeCollection currencyNodes = document.DocumentNode.SelectNodes("//table[@id='paymentDisplayTable']//span[@class='currencyCode']");
                if (currencyNodes != null && currencyNodes.Count > 0)
                {
                    HtmlNode totalSpanNode = currencyNodes.Last();
                    if (totalSpanNode != null)
                    {
                        HtmlNode trPriceNode = totalSpanNode.ParentNode;
                        if (trPriceNode != null)
                        {
                            string totalFee = Regex.Replace(trPriceNode.InnerText, "[^0-9]", "");
                            try
                            {
                                _result = new RegisterResult();
                                _result.BookId = bookId;
                                _result.FeeBeforeTax = feeBeforeTax.ToString();
                                _result.TotalFee = totalFee;
                                _result.Tax = (Convert.ToInt32(totalFee) - feeBeforeTax).ToString();

                            }
                            catch (Exception)
                            {
                                _result = null;
                            }
                            
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
