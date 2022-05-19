using System;
using System.Collections.Generic;

namespace Airline.Models
{
    public partial class AirlineDetails
    {
        public string FlightNumber { get; set; }
        public string Logo { get; set; }
        public string ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public string InstrumentUsed { get; set; }
        public int? BusinessSeats { get; set; }
        public int? NonBusinessSeats { get; set; }
        public double? BaseFare { get; set; }
        public int? BusinessRows { get; set; }
        public int? NonBusinessRows { get; set; }
        public int? Blocked { get; set; }

        public virtual FlightDetails FlightNumberNavigation { get; set; }
    }
}
