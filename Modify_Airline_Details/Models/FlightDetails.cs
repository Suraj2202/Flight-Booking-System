using System;
using System.Collections.Generic;

namespace Modify_Airline_Details.Models
{
    public partial class FlightDetails
    {
        public string FlightNumber { get; set; }
        public string FlightName { get; set; }
        public int DetailsUpdated { get; set; }

        public virtual AirlineDetails AirlineDetails { get; set; }
    }
}
