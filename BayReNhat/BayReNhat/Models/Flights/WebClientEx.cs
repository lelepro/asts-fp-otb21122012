using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;

namespace Flights
{
    class WebClientEx : WebClient
    {
        private string m_address;

        public static WebClientEx CreateMekongAirWebClient(String address)
        {
 
            WebClientEx webClient = new WebClientEx();

            webClient.m_address = address;
            webClient.Headers["Cookie"] = "ASP.NET_SessionId=wnlg1y55kwwpga55bmw21gqt; cae_browser=desktop; sessionculture=vi-VN; multitabtimestamp=1355646292739.4; SiteCore_POS=Vietnam; Country_POS=vn-vi; txtOrigin#01=H%E0%20N%u1ED9i%20%28HAN%29; txtOriginCityCode#01=HAN; txtDestination#01=Tp.H%u1ED3%20Ch%ED%20Minh%20%28SGN%29; txtDestinationCityCode#01=SGN; txtDepart#01=25/12/2012; txtReturn#01=; ddlCurrency#01=VND; ddlAdults#01=1; ddlKids#01=0; rdoFlightTypeReturn#01=undefined; rdoFlightTypeOneWay#01=checked; rdoTravelPrefFixed#01=checked; rdoTravelPrefCheap#01=undefined; compactSearchSubmit#01=1355646407885; __utma=20450727.1494914672.1355320004.1355622868.1355646279.9; __utmb=20450727.6.9.1355646407894; __utmc=20450727; __utmz=20450727.1355646279.9.8.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)";
            
            return webClient;
        }

        public static WebClientEx CreateVNAirlineWebClient(String address)
        {

            WebClientEx webClient = new WebClientEx();

            webClient.m_address = address;
            webClient.Headers["Cookie"] = "BIGipServerprod_2.sabresonicweb_pool_4443=2231161098.23313.0000; BIGipServerprod.sabresonicweb_pool_4443=2801455114.11033.0000; __utma=60035391.1904160844.1355161353.1355739624.1355743791.19; __utmb=60035391.33.10.1355743791; __utmc=60035391; __utmz=60035391.1355743791.19.13.utmcsr=vietnamairlines.com|utmccn=(referral)|utmcmd=referral|utmcct=/wps/portal/vn/site/home";

            return webClient;
        }

        public static WebClientEx CreateJetstarWebClient(String address)
        {

            WebClientEx webClient = new WebClientEx();

            webClient.m_address = address;
            webClient.Headers["Cookie"] = "__utma=187758136.1709446943.1355625279.1355625279.1355625279.1; __utmb=187758136.3.10.1355625279; __utmc=187758136; __utmz=187758136.1355625279.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=mekong%20air;ASP.NET_SessionId=ofdodk2scg2n4vij0tm30aa1";

            return webClient;
        }

        public void DownloadFileEx(string postData, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)GetWebRequest(new Uri(m_address));
            request.ServicePoint.Expect100Continue = false;

            Headers["Content-Type"] = "application/x-www-form-urlencoded";
            byte[] byteRespone = UploadData(m_address, "POST", System.Text.Encoding.UTF8.GetBytes(postData));

            FileStream writeStream = new FileStream(fileName, FileMode.Create);
            BinaryWriter writeBinay = new BinaryWriter(writeStream);
            writeBinay.Write(byteRespone);
            writeBinay.Close();
        }

        public void DownloadFileEx2(string postData, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)GetWebRequest(new Uri(m_address));
            request.ServicePoint.Expect100Continue = false;

            Headers["Content-Type"] = "application/x-www-form-urlencoded";
            byte[] byteRespone = UploadData(m_address, "POST", System.Text.Encoding.Unicode.GetBytes(postData));

            FileStream writeStream = new FileStream(fileName, FileMode.Create);
            BinaryWriter writeBinay = new BinaryWriter(writeStream);
            writeBinay.Write(byteRespone);
            writeBinay.Close();
        }
    }
}
