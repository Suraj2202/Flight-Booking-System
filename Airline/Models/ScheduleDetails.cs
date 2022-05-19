using System;
using System.Collections.Generic;

namespace Airline.Models
{
    public partial class ScheduleDetails
    {
        public string FlightNumber { get; set; }
        public string ConfirmationNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Schedule { get; set; }
        public string Meal { get; set; }
    }
}
