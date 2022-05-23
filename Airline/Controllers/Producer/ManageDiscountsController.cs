using Airline.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airline.Controllers.Producer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageDiscountsController : ControllerBase
    {
        private readonly IBusControl _bus;

        public ManageDiscountsController(IBusControl bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> AirlineData([FromBody] DiscountDetails discountDetails)
        {
            Uri uri = new Uri("rabbitmq://localhost/shared_airline_queue");
            using (InventoryContext context = new InventoryContext())
            {
                DiscountDetails discountDetail = context.DiscountDetails?.Where(x => x.CouponCode == discountDetails.CouponCode).FirstOrDefault();
                var endPoint = await _bus.GetSendEndpoint(uri);
                if (discountDetail != null)
                {
                    SharedAirlineDetails sharedAirlineDetails = new SharedAirlineDetails()
                    {
                        CouponCode = discountDetail.CouponCode,
                        MinimumAmount = discountDetail.MinimumAmount,
                        Value = discountDetail.Value
                    };
                    await endPoint.Send(sharedAirlineDetails);

                    return Ok("Success");
                }
            }
            return BadRequest("Something Went Wrong");
        }
    }
}
