using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modify_Airline_Details.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modify_Airline_Details.Controllers.Discount
{
    [Route("[controller]")]
    [ApiController]
    public class DeleteDiscountDetailsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] DiscountsDetails discount)
        {
            using (InventoryContext ctx = new InventoryContext())
            {
                DiscountsDetails deleteRow = ctx.DiscountsDetails.Where(x => x.CouponCode == discount.CouponCode)?.FirstOrDefault();
                ctx.Remove(deleteRow);
                ctx.SaveChanges();
                return Ok("Success");
            }
        }
    }
}
