using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Flights
{
    class AirMekongDelegate
    {
        protected string _host = "https://booking.airmekong.com.vn/SearchInfo.aspx";


        protected List<Flight> ParseFlightTable(HtmlDocument document, string tableId)
        {
            List<Flight> flights = new List<Flight>();

            string xpath = String.Format("//table[@id='{0}']//tr", tableId);

            HtmlNodeCollection rows = document.DocumentNode.SelectNodes(xpath);
            if (null == rows)
            {
                return flights;
            }

            foreach (HtmlNode row in rows)
            {
                

                HtmlNodeCollection columns = row.SelectNodes("./td");
                if (columns != null && 8 == columns.Count)
                {
                    Flight flight = new Flight();
                    flight.AirlineName = "Mekong Air";

                    HtmlNode flightNoNode = columns[0];
                    HtmlNode linkNode = flightNoNode.SelectSingleNode("./a");
                    if (linkNode != null)
                    {
                        flightNoNode.RemoveChild(flightNoNode.SelectSingleNode("./a"));
                    }
                    
                    string flightNo = flightNoNode.InnerText;
                    flightNo = flightNo.Replace("\r\n ", "");
                    flightNo = flightNo.Replace("  ", "");
                    flightNo = flightNo.Trim();
                    flight.FlightNo = flightNo;

                    
                    int removedLength = 3;
                    if (columns[1] != null)
                    {
                        string text = columns[1].InnerText;
                        flight.StartTime = text.Remove(text.Length - removedLength);
                    }
                    
                    if (columns[2] != null)
                    {
                        string text = columns[2].InnerText;
                        flight.EndTime = text.Remove(text.Length - removedLength);
                    }

                    HtmlNodeCollection pricesNode = row.SelectNodes("./td/p");
                    if (null != pricesNode)
                    {
                        flight.Price = pricesNode.Last().InnerText;
                    }

                    flights.Add(flight);
                }
                
            }
            return flights;
        }

        protected Trip CreateDepartTrip(HtmlDocument document, Parameters param)
        {
            Trip departTrip = new Trip();
            departTrip.DepartCity = param.From;
            departTrip.ReturnCity = param.To;

            List<Flight> flights = ParseFlightTable(document, "availabilityTable0");
            departTrip.Flights = flights;
            return departTrip;
        }
    }
}
