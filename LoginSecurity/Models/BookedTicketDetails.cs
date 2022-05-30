using System;
using System.Collections.Generic;

namespace LoginSecurity.Models
{
    public partial class BookedTicketDetails
    {
        public string UniqueBookingId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Pnrnumber { get; set; }
        public string FlightName { get; set; }
        public string FlightIid { get; set; }
        public string PasangerName { get; set; }
        public string PasangerAge { get; set; }
        public string Gender { get; set; }
        public string SeatNumber { get; set; }
        public string DepartureTime { get; set; }
        public string DepartureFrom { get; set; }
        public string ArrivalTime { get; set; }
        public string ArrivalTo { get; set; }
        public string Fare { get; set; }
        public string CanCancel { get; set; }
        public string CancelStatus { get; set; }
    }
}
