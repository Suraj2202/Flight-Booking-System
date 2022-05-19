using System;
using System.Collections.Generic;

namespace Modify_Airline_Details.Models
{
    public partial class DiscountDetails
    {
        public string CouponCode { get; set; }
        public double? Value { get; set; }
        public int? Avail { get; set; }
    }
}
