using Airline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airline.Controllers.Discount
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddDiscountsController : ControllerBase
    {
        // GET: api/<AddDiscountsController>
        [HttpGet]
        public IEnumerable<DiscountDetails> Get()
        {
            using (InventoryContext context = new InventoryContext())
            {
                return context.DiscountDetails.ToList();
            }
        }

        // POST api/<AddDiscountsController>
        [HttpPost]
        public void Post([FromBody] DiscountDetails discountDetails)
        {
            DiscountDetails discountDetail = new DiscountDetails()
            {
                CouponCode = discountDetails.CouponCode,
                MinimumAmount = discountDetails.MinimumAmount,
                Value = discountDetails.Value
            };

                //adding to Discount Details DB
                using (InventoryContext context = new InventoryContext())
                {
                    context.DiscountDetails.Add(discountDetail);

                    context.SaveChanges();                    

                }            
        }
    }
}
