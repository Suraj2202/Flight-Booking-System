using Microsoft.AspNetCore.Mvc;
using Modify_Airline_Details.FieldSetting;
using Modify_Airline_Details.Models;
using System.Linq;


namespace Modify_Airline_Details.Controllers.Schedule
{
    [Route("[controller]")]
    [ApiController]
    public class UpdateScheduleDetailsController : ControllerBase
    {

        #region //private variables

        private IAirlineData airlineData = new AirlineData();

        #endregion
        // POST api/<UpdateScheduleDetailsController>
        [HttpPost("{checkValue}")]
        public IActionResult Post(string checkValue,[FromBody] SchedulesDetails scheduleDetails)
        {
            using (InventoryContext context = new InventoryContext())
            {
                SchedulesDetails airline = context.SchedulesDetails.Where(x => x.ConfirmationNumber == checkValue).FirstOrDefault();
                if (airline != null)
                {
                    if (scheduleDetails.FlightNumber != null)
                        airline.FlightNumber = scheduleDetails.FlightNumber;
                    if (scheduleDetails.From != null)
                        airline.From = scheduleDetails.From;
                    if (scheduleDetails.To != null)
                        airline.To = scheduleDetails.To;
                    if (scheduleDetails.StartDateTime != null)
                        airline.StartDateTime = scheduleDetails.StartDateTime;
                    if (scheduleDetails.EndDateTime != null)
                        airline.EndDateTime = scheduleDetails.EndDateTime;
                    if (scheduleDetails.Schedule != null)
                        airline.Schedule = scheduleDetails.Schedule;
                    if (scheduleDetails.Meal != null)
                        airline.Meal = scheduleDetails.Meal;
                    
                    // update all the values passed from 
                    context.SaveChanges();
                    return Ok("Success");
                }
                else
                    return BadRequest(checkValue);
            }
        }

    }
}
