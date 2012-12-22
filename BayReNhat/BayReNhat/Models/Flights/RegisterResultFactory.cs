using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Flights
{
    class RegisterResultFactory
    {
        public string GenerateJsonResult(FlightRegisterParameters parameters)
        {
            FlightRegister register = GetRegister(parameters);
            if (register != null)
            {
                register.Register(parameters);
                RegisterResult result = register.GetResult();
                if (result != null)
                {
                    string json = JsonConvert.SerializeObject(result, Formatting.None);
                    return json;
                }
            }
            return "";
        }
        protected FlightRegister GetRegister(FlightRegisterParameters parameters)
        {
            string type = Regex.Match(parameters.DepartFlightNo, @"[A-Za-z]+").Value;
            if (type.Contains("VN") == true)
            {
                return new VNAirlineRegister();
            }
            else if (type.Contains("BL") == true)
            {
                return new JestaRegister();
            }
            else if (type.Contains("P8") == true)
            {
                return null;
            }
            return null;
        }

    }
}
