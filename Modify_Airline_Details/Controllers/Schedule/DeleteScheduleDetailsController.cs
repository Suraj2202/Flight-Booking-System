using Microsoft.AspNetCore.Mvc;
using Modify_Airline_Details.Models;
using System.Linq;

namespace Modify_Airline_Details.Controllers.Schedule
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteScheduleDetailsController : ControllerBase
    {
        // POST : api/<DeleteScheduleDetailsController>
        [HttpPost]
        public IActionResult Post([FromBody] SchedulesDetails schedule)
        {
            using(InventoryContext ctx = new InventoryContext())
            {
                SchedulesDetails deleteRow = ctx.SchedulesDetails.Where(x => x.ConfirmationNumber == schedule.ConfirmationNumber)?.FirstOrDefault();
                ctx.Remove(deleteRow);
                ctx.SaveChanges();
                return Ok("Success");
            }
        }
    }
}
