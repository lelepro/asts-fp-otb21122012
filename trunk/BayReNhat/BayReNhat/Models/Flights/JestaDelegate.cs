using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Flights
{
    class JestaDelegate
    {
        protected string _host = "http://booknow.jetstar.com/Search.aspx?culture=vi-VN";

        protected List<Flight> ParseFlightTable(HtmlDocument document, int index)
        {
            List<Flight> flights = new List<Flight>();

            HtmlNodeCollection tables = document.DocumentNode.SelectNodes("//table[@class='domestic']");
            if (null == tables)
            {
                return flights;
            }

            HtmlNode departTable = tables.ElementAt(index);
            if (null == departTable)
            {
                return flights;
            }

            HtmlNode tableBody = departTable.SelectSingleNode("./tbody");
            if (null == tableBody)
            {
                return flights;
            }

            HtmlNode unnecessaryNode = departTable.SelectSingleNode(".//tr[@class='starter-options ']");
            if (unnecessaryNode != null)
            {
                tableBody.RemoveChild(unnecessaryNode);
            }

            unnecessaryNode = departTable.SelectSingleNode(".//tr[@class='business-options ']");
            if (unnecessaryNode != null)
            {
                tableBody.RemoveChild(unnecessaryNode);
            }

            HtmlNodeCollection rows = tableBody.SelectNodes("./tr");
            if (rows == null)
            {
                return flights;
            }

            foreach (var row in rows)
            {
                Flight flight = new Flight();

                HtmlNode node = row.SelectSingleNode("./td[1]/strong");
                if (node != null)
                {
                    flight.StartTime = node.InnerText;
                }

                node = row.SelectSingleNode("./td[2]/strong");
                if (node != null)
                {
                    flight.EndTime = node.InnerText;
                }

                node = row.SelectSingleNode("./td[4]//label");
                
                if (node != null)
                {
                    flight.Price = node.InnerText;
                }
                

                HtmlNode flightNoNode = row.SelectSingleNode(".//span[@class='flight-no']");
                if (flightNoNode != null)
                {
                    unnecessaryNode = flightNoNode.SelectSingleNode("./img");
                    if (unnecessaryNode != null)
                    {
                        flightNoNode.RemoveChild(unnecessaryNode);
                    }
                    flight.FlightNo = flightNoNode.InnerText.Replace("  ", " ");

                    flight.AirlineName = "Jesta";
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

            List<Flight> flights = ParseFlightTable(document, 0);
            departTrip.Flights = flights;
            return departTrip;
        }
    }
}
