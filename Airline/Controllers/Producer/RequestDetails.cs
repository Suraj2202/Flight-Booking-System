using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airline.Controllers.Producer
{
    public class RequestDetails
    {
        public string UserName { get; set; }
        public string To { get; set; }
        public string From { get; set; }

        //format "MM/dd/yyyy HH:mm:ss"
        public string DepartStartDateTime { get; set; }
        public string ReturnStartDateTime { get; set; }
    }
}
