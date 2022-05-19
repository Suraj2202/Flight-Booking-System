﻿using Airline.FieldSetting;
using Airline.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleAirlineController : ControllerBase
    {

        #region
        //private variables
        private IAirlineData airlineData = new AirlineData();
        #endregion

        // GET: api/<ScheduleAirlineController>
        [HttpGet]
        public IEnumerable<ScheduleDetails> Get()
        {
            using (InventoryContext context = new InventoryContext())
            {
                return context.ScheduleDetails.ToList();
            }
        }

        // POST api/<ScheduleAirlineController>
        [HttpPost]
        public void Post([FromBody] ScheduleDetails scheduleDetails)
        {
            if(scheduleDetails.FlightNumber != null)
            {
                ScheduleDetails details = new ScheduleDetails()
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

                //adding 
                using(InventoryContext ctx = new InventoryContext())
                {
                    ctx.ScheduleDetails.Add(details);
                    ctx.SaveChanges();
                }
            }
        }
        
    }
}