﻿using System;
using System.Collections.Generic;

namespace BookTicket.Models
{
    public partial class FlightScheduleDetails
    {
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
        public double BaseFare { get; set; }
    }
}
