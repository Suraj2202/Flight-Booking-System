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

        [HttpPost]
        public IActionResult Post([FromBody] DiscountDetails discountDetails)
        {
            using (InventoryContext context = new InventoryContext())
            {
                DiscountDetails discount = context.DiscountDetails.Where(x => x.CouponCode == discountDetails.CouponCode).FirstOrDefault();
                if (discountDetails.CouponCode != null && discount != null)
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
                    return BadRequest();
            }
        }
    }
}
