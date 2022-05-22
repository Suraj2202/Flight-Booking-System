using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared_Model
{
    public class SharedScheduleDetails
    {
        public string UserName { get; set; }
        public string UniqueKey { get; set; }
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
