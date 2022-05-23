﻿using BookTicket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneWaySearchFlightsController : ControllerBase
    {
        // GET: api/<OneWaySearchFlightsController>
        [HttpGet]
        public IEnumerable<FlightSchedules> Get(FlightSchedules flight)
        {
            using (UserSideContext context = new UserSideContext())
            {
                return context.FlightSchedules.Where(x=> x.UserName == flight.UserName && x.UniqueKey == flight.UniqueKey).ToList();
            }
        }
    }
}
