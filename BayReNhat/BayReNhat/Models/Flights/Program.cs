using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using HtmlAgilityPack;
namespace Flights
{
    class Program
    {
        static void Main(string[] args)
        {

            //Parameters parameters = new Parameters();
            //parameters.From = "SGN";
            //parameters.To = "HAN";
            //parameters.DepartDay = "20";
            //parameters.DepartMonth = "12";
            //parameters.DepartYear = "2012";
            //parameters.ReturnDay = "30";
            //parameters.ReturnMonth = "12";
            //parameters.ReturnYear = "2012";
            //parameters.BookingType = "roundtrip";
            ////parameters.BookingType = "oneway";

            //AirlineInfoFactory airlineInfoFactory = new AirlineInfoFactory();
            //string json = airlineInfoFactory.GenerateJsonResult(parameters);
            //StreamWriter writer = new StreamWriter(new FileStream("d:\\result.txt", FileMode.Create));
            //writer.Write(json);
            //writer.Close();

            FlightRegisterParameters parameters = new FlightRegisterParameters();
            parameters.From = "SGN";
            parameters.To = "HAN";
            parameters.DepartDay = "1";
            parameters.DepartMonth = "1";
            parameters.DepartYear = "2013";
            parameters.ReturnDay = "5";
            parameters.ReturnMonth = "1";
            parameters.ReturnYear = "2013";
            parameters.DepartFlightNo = "BL 802";
            parameters.ReturnFlightNo = "BL 799";
            parameters.BookingType = "roundtrip";
            //parameters.BookingType = "oneway";
            //VN Airline ko cho nhap ten co dau
            parameters.FirstName = "Nguyen";
            parameters.LastName = "Van Quy";
            parameters.Phone = "0998 43353";
            parameters.Email = "abc@gmail.com";

            RegisterResultFactory registerResultFactory = new RegisterResultFactory();
            string json = registerResultFactory.GenerateJsonResult(parameters);

            StreamWriter writer = new StreamWriter(new FileStream("d:\\result.txt", FileMode.Create));
            writer.Write(json);
            writer.Close();

            Console.WriteLine("OK");

        }
    }
}

//JSON Encode
            //ReturnData returnData = new ReturnData();
            //returnData.BookingType = "oneway";
            //List<Trip> trips = new List<Trip>();
            //returnData.Data = trips;

            //Trip trip1 = new Trip();
            //Trip trip2 = new Trip();
            //trips.Add(trip1);
            //trips.Add(trip2);

            //trip1.DepartCity = "HN";
            //trip1.ReturnCity = "HCM";
            //trip2.DepartCity = "HN";
            //trip2.ReturnCity = "HCM";

            //List<Flight> flights = new List<Flight>();

            //Flight flight = null;
            //flight = new Flight();
            //flight.FlightNo = "VN 1376";
            //flight.AirlineName = "Vietnam Airline";
            //flight.StartTime = "19:00";
            //flight.EndTime = "21:00";
            //flight.Price = "979.000 VND";
            //flights.Add(flight);

            //flight = new Flight();
            //flight.FlightNo = "VN 1376";
            //flight.AirlineName = "Vietnam Airline";
            //flight.StartTime = "19:00";
            //flight.EndTime = "21:00";
            //flight.Price = "979.000 VND";
            //flights.Add(flight);

            //trip1.Flights = flights;
            //trip2.Flights = flights;

            //string output = JsonConvert.SerializeObject(returnData);
//JSON Encode


//HttpWebRequest
//byte[] byteRespone = web.UploadData(host, "POST", System.Text.Encoding.UTF8.GetBytes(postData));
//Console.WriteLine(byteRespone.Length);

//FileStream writeStream = new FileStream("d:\\abc.html", FileMode.Create);
//BinaryWriter writeBinay = new BinaryWriter(writeStream);
//writeBinay.Write(byteRespone);

//string uri = "https://booking.airmekong.com.vn/SearchInfo.aspx";
//HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
//request.Method = "POST";

//String postData = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGRk77h9CNekodLMZhdJu3riPx9ebN8%3D&pageToken=&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24RadioButtonMarketStructure=OneWay&originStation1=SGN&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketOrigin1=SGN&destinationStation1=HAN&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketDestination1=HAN&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDay1=20&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketMonth1=2012-12&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDateRange1=0%7C4&originStation2=&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketOrigin2=%C4%90i%E1%BB%83m+%C4%91i...&destinationStation2=&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketDestination2=%C4%90i%E1%BB%83m+%C4%91%E1%BA%BFn...&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDay2=22&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketMonth2=2012-12&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDateRange2=0%7C4&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListPassengerType_ADT=1&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListPassengerType_CHD=0&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListPassengerType_INFANT=0&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListSearchBy=columnView&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24ButtonSubmit=T%C3%ACm+chuy%E1%BA%BFn+bay";
//byte[] byteArray = Encoding.UTF8.GetBytes(postData);


//request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
//request.Referer = "https://booking.airmekong.com.vn/SearchInfo.aspx?Culture=vi-Vn";
//request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
//request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
//request.Headers.Add(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8;q=0.7,*;q=0.3");
//request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8");
//request.Headers.Add(HttpRequestHeader.Via);

//request.ServicePoint.Expect100Continue = false;
//request.CookieContainer = new CookieContainer();
//request.CookieContainer.Add(new Uri(uri), new Cookie("ASP.NET_SessionId", "maebhn55333a1unuwov03z45"));
//request.CookieContainer.Add(new Uri(uri), new Cookie("__utma", "187758136.1446620650.1355320027.1355536701.1355581306.4"));
//request.CookieContainer.Add(new Uri(uri), new Cookie("__utmb", "187758136.4.10.1355581306"));
//request.CookieContainer.Add(new Uri(uri), new Cookie("__utmc", "187758136"));
//request.CookieContainer.Add(new Uri(uri), new Cookie("__utmz", "187758136.1355581306.4.4.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)"));

//request.Headers.Add("Cache-Control", "max-age=0");
////request.Referer = "https://booking.airmekong.com.vn/SearchInfo.aspx?Culture=vi-Vn";

//request.ContentType = "application/x-www-form-urlencoded";
//request.ContentLength = byteArray.Length;

//Stream dataStream = request.GetRequestStream();
//dataStream.Write(byteArray, 0, byteArray.Length);
//dataStream.Close();

//WebResponse response = request.GetResponse();

//Stream strm = response.GetResponseStream();

//StreamReader reader = new StreamReader(strm);
//StreamWriter writer = new StreamWriter("d:\\xxx.html");

//string line;
//while ((line = reader.ReadLine()) != null)
//{
//    writer.WriteLine(line);
//}

//response.Close();

//WebRequest request = WebRequest.Create("https://booking.airmekong.com.vn/SearchInfo.aspx");
//request.Method = "POST";

//String postData = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUBMGRk77h9CNekodLMZhdJu3riPx9ebN8%3D&pageToken=&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24RadioButtonMarketStructure=OneWay&originStation1=SGN&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketOrigin1=SGN&destinationStation1=HAN&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketDestination1=HAN&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDay1=20&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketMonth1=2012-12&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDateRange1=0%7C4&originStation2=&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketOrigin2=%C4%90i%E1%BB%83m+%C4%91i...&destinationStation2=&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24TextBoxMarketDestination2=%C4%90i%E1%BB%83m+%C4%91%E1%BA%BFn...&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDay2=22&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketMonth2=2012-12&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListMarketDateRange2=0%7C4&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListPassengerType_ADT=1&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListPassengerType_CHD=0&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListPassengerType_INFANT=0&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24DropDownListSearchBy=columnView&ControlGroupSearchInfoView%24AvailabilitySearchInputSearchInfoView%24ButtonSubmit=T%C3%ACm+chuy%E1%BA%BFn+bay";
//byte[] byteArray = Encoding.UTF8.GetBytes(postData);

//request.ContentType = "application/x-www-form-urlencoded";
//request.ContentLength = byteArray.Length;

//Stream dataStream = request.GetRequestStream();
//dataStream.Write(byteArray, 0, byteArray.Length);
//dataStream.Close();

//WebResponse response = request.GetResponse();
//response.Close();

//Console.WriteLine(((HttpWebResponse)response).StatusDescription);

//Stream strm = response.GetResponseStream();
//BinaryReader reader = new BinaryReader(strm);

//FileStream writeStream = new FileStream("d:\\abc.html", FileMode.Create);
//BinaryWriter writeBinay = new BinaryWriter(writeStream);

//const int BUFFER_LENGTH = 4096;
//byte[] buffer = new byte[BUFFER_LENGTH];

//int total = 0;
//int count = 0;
//while ((count = reader.Read(buffer, 0, BUFFER_LENGTH)) != 0)
//{
//    writeBinay.Write(buffer);
//    //total += count;
//}
//Console.WriteLine(total);
//writeBinay.Close();
//reader.Close();
//response.Close();
////const string saveFilePath = "D:\\xxx.html";
//httpWebRequest