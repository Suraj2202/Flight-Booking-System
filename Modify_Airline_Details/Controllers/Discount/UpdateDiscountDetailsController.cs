using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modify_Airline_Details.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modify_Airline_Details.Controllers.Discount
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateDiscountDetailsController : ControllerBase
    {

        [HttpPost("{checkValue}")]
        public IActionResult Post(string checkValue,[FromBody] DiscountsDetails discountDetails)
        {
            using (InventoryContext context = new InventoryContext())
            {
                DiscountsDetails discount = context.DiscountsDetails.Where(x => x.CouponCode == checkValue).FirstOrDefault();
                if (discount != null)
                {
                    if (discountDetails.CouponCode != null)
                        discount.CouponCode = discountDetails.CouponCode;
                    if (discountDetails.Value != null)
                        discount.Value = discountDetails.Value;
                    if (discountDetails.MinimumAmount != null)
                        discount.MinimumAmount = discountDetails.MinimumAmount;

                    // update all the values passed from 
                    context.SaveChanges();
                    return Ok("Success");
                }
                else
                    return BadRequest("Coupon Code not found");
            }
        }
    }
}
