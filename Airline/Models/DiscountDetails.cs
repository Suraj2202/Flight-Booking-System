using System;
using System.Collections.Generic;

namespace Airline.Models
{
    public partial class DiscountDetails
    {
        public string CouponCode { get; set; }
        public double? Value { get; set; }
        public double? MinimumAmount { get; set; }
    }
}
