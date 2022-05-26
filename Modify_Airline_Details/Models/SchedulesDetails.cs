using System;
using System.Collections.Generic;

namespace Modify_Airline_Details.Models
{
    public partial class SchedulesDetails
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
