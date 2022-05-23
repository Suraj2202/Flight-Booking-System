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
    public class DeleteDiscountDetailsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] DiscountDetails discount)
        {
            using (InventoryContext ctx = new InventoryContext())
            {
                DiscountDetails deleteRow = ctx.DiscountDetails.Where(x => x.CouponCode == discount.CouponCode)?.FirstOrDefault();
                ctx.Remove(deleteRow);

                return Ok("Success");
            }
        }
    }
}
