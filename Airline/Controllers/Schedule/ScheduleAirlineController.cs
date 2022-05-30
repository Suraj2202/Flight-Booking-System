using Airline.FieldSetting;
using Airline.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airline.Controllers.Schedule
{
    [Route("[controller]")]
    [ApiController]
    public class ScheduleAirlineController : ControllerBase
    {

        #region
        //private variables
        private IAirlineData airlineData = new AirlineData();
        #endregion

        // GET: api/<ScheduleAirlineController>
        [HttpGet]
        public IEnumerable<SchedulesDetails> Get()
        {
            using (InventoryContext context = new InventoryContext())
            {
                return context.SchedulesDetails.ToList();
            }
        }

        // GET: Single Data
        [HttpGet("{schedulesDetails}")]
        public SchedulesDetails Get(string schedulesDetails)
        {
            using (InventoryContext context = new InventoryContext())
            {
                return context.SchedulesDetails?.Where(x => x.ConfirmationNumber == schedulesDetails).FirstOrDefault();
            }
        }

        // POST api/<ScheduleAirlineController>
        [HttpPost]
        public IActionResult Post([FromBody] SchedulesDetails scheduleDetails)
        {
            if(scheduleDetails.FlightNumber != null)
            {                
                //adding 
                using(InventoryContext ctx = new InventoryContext())
                {
                    SchedulesDetails details = new SchedulesDetails()
                    {
                        FlightNumber = scheduleDetails.FlightNumber,
                        ConfirmationNumber = airlineData.GetConfirmationNumber(scheduleDetails.FlightNumber),
                        From = scheduleDetails.From,
                        To = scheduleDetails.To,
                        StartDateTime = scheduleDetails.StartDateTime,
                        EndDateTime = scheduleDetails.EndDateTime,
                        Schedule = scheduleDetails.Schedule,
                        Meal = scheduleDetails.Meal
                    };
                    
                    ctx.SchedulesDetails.Add(details);
                    ctx.SaveChanges();
                    return Ok("Success");
                }
            }
            return BadRequest();
        }
        
    }
}
