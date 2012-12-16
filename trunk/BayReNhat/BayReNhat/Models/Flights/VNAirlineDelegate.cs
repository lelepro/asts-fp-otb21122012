using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Flights
{
    abstract class VNAirlineDelegate
    {
        protected Dictionary<string, string> _monthMap;

        public VNAirlineDelegate()
        {
            _monthMap = new Dictionary<string, string>();
            _monthMap.Add("1", "JAN");
            _monthMap.Add("2", "FEB");
            _monthMap.Add("3", "MAR");
            _monthMap.Add("4", "APR");
            _monthMap.Add("5", "MAY");
            _monthMap.Add("6", "JUN");
            _monthMap.Add("7", "JUL");
            _monthMap.Add("8", "AUG");
            _monthMap.Add("9", "SEP");
            _monthMap.Add("10", "OCT");
            _monthMap.Add("11", "NOV");
            _monthMap.Add("12", "DEC");
        }
 
        protected void Normalize(Parameters param)
        {
            param.DepartMonth = _monthMap[param.DepartMonth];
            param.ReturnMonth = _monthMap[param.ReturnMonth];
        }

        protected List<Flight> ParseFlightTable(HtmlDocument document, string tableId)
        {
            List<Flight> flights = new List<Flight>();
            string xpath = String.Format("//table[@id='{0}']/tbody/tr", tableId);

            HtmlNodeCollection rowCollection = document.DocumentNode.SelectNodes(xpath);
            if (null == rowCollection)
            {
                return flights;
            }


            foreach (HtmlNode row in rowCollection)
            {
                Flight flight = new Flight();

                flight.AirlineName = "Vietnam Airline";

                HtmlNode node = null;

                node = row.SelectSingleNode(".//div[@class='depTimeSR']/p[@class='highlight']");
                if (node != null)
                {
                    flight.StartTime = node.InnerText;
                }

                node = row.SelectSingleNode(".//div[@class='arrTimeSR']/p[@class='highlight']");
                if (node != null)
                {
                    flight.EndTime = node.InnerText;
                }

                node = row.SelectSingleNode(".//span[@class='matrix_airline_code']");
                if (node != null)
                {
                    flight.FlightNo = Utilities.RemoveNonBreakingSpace(node.InnerText);
                }

                HtmlNodeCollection priceColletion = row.SelectNodes(".//span[@class='step2PricePoints ']//a");
                node = priceColletion.Last();
                flight.Price = node.InnerText;

                flights.Add(flight);
            }
            return flights;
        }

        protected Trip CreateDepartTrip(HtmlDocument document, Parameters param)
        {
            Trip departTrip = new Trip();
            departTrip.DepartCity = param.From;
            departTrip.ReturnCity = param.To;


            List<Flight> flights = ParseFlightTable(document, "bfm_tbl_out");
            departTrip.Flights = flights;
            return departTrip;
        }
    

    }
}
