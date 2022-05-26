using Airline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airline.FieldSetting
{
    public class AirlineData : IAirlineData
    {
        public string GetAirlineLogoPath(string flightNumber)
        {
            if (flightNumber != null && DomesticAirlineName().ContainsKey(flightNumber.Substring(0,2)))
                return @".\Images\Logos\" + flightNumber.Substring(0, 2) + ".png";
            else
                return "";
        }

        public string GetFlightName(string flightNumber)
        {
            Dictionary<string, string> domesticAirlineName = DomesticAirlineName();
            string flightKey = flightNumber.Substring(0, 2);
            string airlineName = "";
            if (flightNumber != null && domesticAirlineName.ContainsKey(flightKey))
            {
                airlineName = domesticAirlineName[flightKey];
            }
            return airlineName;
        }

        public bool IsFlightNumberAvailable(string flightNumber)
        {

            string flightKey = flightNumber.Substring(0, 2);
            if (DomesticAirlineName().ContainsKey(flightKey))
                return true;

            return false;
        }

        //Dict of Domestic Airline Code and Airline Name
        private Dictionary<string, string> DomesticAirlineName()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("I5", "AirAsia India");
            keyValuePairs.Add("AI", "Air India");
            keyValuePairs.Add("IX", "Air India Express");
            keyValuePairs.Add("G8", "Go First");
            keyValuePairs.Add("6E", "IndiGo");
            keyValuePairs.Add("SG", "SpiceJet");
            keyValuePairs.Add("UK", "Vistara");

            return keyValuePairs;
        }

        public string GetConfirmationNumber(string flightNumber)
        {
            string flightKey = flightNumber.Substring(0, 2);           
            string confirmationNumber = "";

            //Generate Unique key number for Flight Schedule           
            if(flightNumber != null)
            {
                confirmationNumber = flightKey + "-" + DateTime.Now.ToString("yyyyMMdd:HHmm").Replace('/', ' ');
            }

            //check if same confirmation code is there or not
            using(InventoryContext ctx = new InventoryContext())
            {
                bool changeAccepted = false;
                while(changeAccepted != true)
                {
                    string checkValue = ctx.SchedulesDetails.Where(x => x.ConfirmationNumber == confirmationNumber)?.FirstOrDefault()?.ConfirmationNumber;

                    if (checkValue != "")
                    {
                        changeAccepted = true;
                    }
                    else
                    {
                        int changeLastId = int.Parse(checkValue.Substring(16)) + 1;
                        confirmationNumber = confirmationNumber.Substring(0,16) + changeLastId;                        
                    }
                }                
            }
            return confirmationNumber;
        }

        public int ConvertStringToInt(string value)
        {
            return int.Parse(value);
        }

        public double ConvertStringToDouble(string value)
        {
            return double.Parse(value);
        }
    }
}
