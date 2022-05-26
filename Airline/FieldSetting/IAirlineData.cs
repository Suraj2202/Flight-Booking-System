using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airline.FieldSetting
{
    interface IAirlineData
    {
        public string GetFlightName(string flightNumber);

        public string GetAirlineLogoPath(string flightNumber);

        public bool IsFlightNumberAvailable(string flightNumber);

        public string GetConfirmationNumber(string flightNumber);

        public int ConvertStringToInt(string value);

        public double ConvertStringToDouble(string value);


    }
}
