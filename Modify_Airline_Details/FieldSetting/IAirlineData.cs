using System.IO;

namespace Modify_Airline_Details.FieldSetting
{
    interface IAirlineData
    {
        public string GetFlightName(string flightNumber);

        public string GetAirlineLogoPath(string flightNumber);

        public bool IsFlightNumberAvailable(string flightNumber);

        public string GetConfirmationNumber(string flightNumber);

    }
}
