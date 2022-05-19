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
        public void Post([FromBody] ScheduleDetails schedule)
        {
            using(InventoryContext ctx = new InventoryContext())
            {
                ScheduleDetails deleteRow = ctx.ScheduleDetails.Where(x => x.ConfirmationNumber == schedule.ConfirmationNumber)?.FirstOrDefault();
                ctx.Remove(deleteRow);

                return;
            }
        }
    }
}
