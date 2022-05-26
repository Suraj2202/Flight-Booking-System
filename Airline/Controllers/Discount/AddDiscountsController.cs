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
        public IEnumerable<DiscountsDetails> Get()
        {
            using (InventoryContext context = new InventoryContext())
            {
                return context.DiscountsDetails.ToList();
            }
        }

        // GET: Single Data
        [HttpGet("{couponCode}")]
        public DiscountsDetails Get(string couponCode)
        {
            using (InventoryContext context = new InventoryContext())
            {
                return context.DiscountsDetails?.Where<DiscountsDetails>(x => 
                                                                         x.CouponCode == couponCode)
                                                                         .FirstOrDefault();
            }
        }

        // POST api/<AddDiscountsController>
        [HttpPost]
        public IActionResult Post([FromBody] DiscountsDetails discountDetails)
        {
            DiscountsDetails discountDetail = new DiscountsDetails()
            {
                CouponCode = discountDetails.CouponCode,
                MinimumAmount = discountDetails.MinimumAmount,
                Value = discountDetails.Value
            };

                //adding to Discount Details DB
                using (InventoryContext context = new InventoryContext())
                {
                    context.DiscountsDetails.Add(discountDetail);

                    context.SaveChanges();
                return Ok("Success");
                }            
        }
    }
}
