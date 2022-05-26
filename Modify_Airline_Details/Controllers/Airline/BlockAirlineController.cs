using Microsoft.AspNetCore.Mvc;
using Modify_Airline_Details.Models;
using System.Linq;

namespace Modify_Airline_Details.Controllers.Airline
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockAirlineController : ControllerBase
    {
        // POST: /api/<BlockAirlineController>
        public IActionResult Post([FromBody] AirlinesDetails details)
        {
            using(InventoryContext ctx = new InventoryContext())
            {
                AirlinesDetails blockAirline = ctx.AirlinesDetails.Where(x => x.FlightNumber == details.FlightNumber)?.FirstOrDefault();
                blockAirline.Blocked = "1";
                ctx.SaveChanges();
                return Ok("Success");
            }
        }
    }
}
