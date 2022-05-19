using Microsoft.AspNetCore.Mvc;
using Modify_Airline_Details.FieldSetting;
using Modify_Airline_Details.Models;
using System.Linq;


namespace Modify_Airline_Details.Controllers.Schedule
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateScheduleDetailsController : ControllerBase
    {

        #region //private variables

        private IAirlineData airlineData = new AirlineData();

        #endregion
        // POST api/<UpdateScheduleDetailsController>
        [HttpPost]
        public void Post([FromBody] ScheduleDetails scheduleDetails)
        {
            using (InventoryContext context = new InventoryContext())
            {
                ScheduleDetails airline = context.ScheduleDetails.Where(x => x.ConfirmationNumber == scheduleDetails.ConfirmationNumber).FirstOrDefault();
                if (scheduleDetails.FlightNumber != null && airline != null)
                {
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
                    return;
                }
                else
                    return;
            }
        }

    }
}
