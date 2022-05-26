using System;
using System.Collections.Generic;

namespace Modify_Airline_Details.Models
{
    public partial class AirlinesDetails
    {
        public string FlightNumber { get; set; }
        public string Logo { get; set; }
        public string ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public string InstrumentUsed { get; set; }
        public string BusinessSeats { get; set; }
        public string NonBusinessSeats { get; set; }
        public string BaseFare { get; set; }
        public string BusinessRows { get; set; }
        public string NonBusinessRows { get; set; }
        public string Blocked { get; set; }
    }
}
