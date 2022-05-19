using System;
using System.Collections.Generic;

namespace Airline.Models
{
    public partial class FlightDetails
    {
        public string FlightNumber { get; set; }
        public string FlightName { get; set; }
        public int DetailsUpdated { get; set; }

        public virtual AirlineDetails AirlineDetails { get; set; }
    }
}
