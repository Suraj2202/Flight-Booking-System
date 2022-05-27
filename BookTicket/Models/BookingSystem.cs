using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTicket.Models
{
    public class BookingSystem : IBookingSystem
    {
        public string PNRNumberGeneration(string flightID)
        {
            return flightID + "-" + Guid.NewGuid().ToString().ToUpper().Replace('-',' ').Trim();            
        }
    }
}
