using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    abstract class FlightRegister
    {
        protected RegisterResult _result = null;

        public abstract void Register(FlightRegisterParameters param);
        public RegisterResult GetResult()
        {
            return _result;
        }
    }
}
