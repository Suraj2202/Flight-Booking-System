using System;
using System.Collections.Generic;

namespace Modify_Airline_Details.Models
{
    public partial class DiscountsDetails
    {
        public string CouponCode { get; set; }
        public string Value { get; set; }
        public string MinimumAmount { get; set; }
    }
}
