using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Flights
{
    class AirlineInfoFactory
    {
        protected static string[] _webScrappingName = {"Vietnam Airline", "Jetstar", "Mekong Air"};
        //protected static string[] _webScrappingName = { "Jetstar" };

        public string GenerateJsonResult(Parameters param)
        {
            List<ReturnData> listFullResult = new List<ReturnData>();
			string departDay = string.Format("{0}/{1}/{2}",
					param.DepartMonth, param.DepartDay, param.DepartYear);
			string returnDay = string.Format("{0}/{1}/{2}",
					param.ReturnMonth, param.ReturnDay, param.ReturnYear);

            for (int i = 0; i < _webScrappingName.Length; i++)
            {
                try
                {
                    WebScraping webScraping = GetWebScrapping(_webScrappingName[i]);
                    webScraping.Parameters = param;

                    string saveFilePath = Path.GetTempFileName();
                    webScraping.DownloadWebPage(saveFilePath);

                    ReturnData returnData = webScraping.ParseResult(saveFilePath);
                    listFullResult.Add(returnData);

                    File.Delete(saveFilePath);
                }

                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            ReturnData finalResult = new ReturnData();
            finalResult.BookingType = param.BookingType;
            finalResult.Data = new List<Trip>();

            Trip departTrip = new Trip();
            departTrip.Flights = new List<Flight>();
            departTrip.DepartCity = param.From;
            departTrip.ReturnCity = param.To;

            Trip returnTrip = new Trip();
            returnTrip.Flights = new List<Flight>();
            returnTrip.DepartCity = param.To;
            returnTrip.ReturnCity = param.From;


            foreach (ReturnData returnData in listFullResult)
            {
                if (returnData.Data.Count >= 1)
                {
                    departTrip.Flights.AddRange(returnData.Data.ElementAt(0).Flights);
                }
                if (returnData.Data.Count >= 2)
                {
                    returnTrip.Flights.AddRange(returnData.Data.ElementAt(1).Flights);
                }
            }
            if (departTrip.Flights.Count > 0)
            {
	            departTrip.Date = departDay;
                 finalResult.Data.Add(departTrip);
            }
            if (returnTrip.Flights.Count > 0)
            {
	            returnTrip.Date = returnDay;
                finalResult.Data.Add(returnTrip);
            }
   
            string json = JsonConvert.SerializeObject(finalResult, Formatting.None);
            return json;
        }

        public static WebScraping GetWebScrapping(string name)
        {
            switch (name)
            {
                case "Vietnam Airline":
                    return new VnAirlineWebScraping();

                case "Jetstar":
                    return new JestaWebScraping();

                case "Mekong Air":
                    return new AirMekongWebSraping();
            }
            return null;
        }

    }
}
