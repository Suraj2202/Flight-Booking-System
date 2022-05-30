using System;
using System.Collections.Generic;

namespace LoginSecurity.Models
{
    public partial class FlightsSchedules
    {
        public string EntryId { get; set; }
        public string FlightNumber { get; set; }
        public string ConfirmationNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Schedule { get; set; }
        public string Meal { get; set; }
        public string UserName { get; set; }
        public string UniqueKey { get; set; }
        public string BaseFare { get; set; }
        public string FlightName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public string InstrumentUsed { get; set; }
        public string BusinessRows { get; set; }
        public string NonBusinessRows { get; set; }
        public string BusinessSeats { get; set; }
        public string NonBusinessSeats { get; set; }
        public string ClassSelected { get; set; }
    }
}
