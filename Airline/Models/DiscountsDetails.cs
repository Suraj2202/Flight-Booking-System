using System;
using System.Collections.Generic;

namespace Airline.Models
{
    public partial class DiscountsDetails
    {
        public string CouponCode { get; set; }
        public string Value { get; set; }
        public string MinimumAmount { get; set; }
    }
}
