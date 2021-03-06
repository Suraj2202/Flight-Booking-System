using BookTicket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTicket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OneWaySearchFlightsController : ControllerBase
    {
        // GET: api/<OneWaySearchFlightsController>
        //api/person/byflight?userName=value&uniqueKey=b
        [HttpGet("flight")]
        public IEnumerable<FlightsSchedules> Get(string userName, string uniqueKey)
        {
            using (UserSideContext context = new UserSideContext())
            {
                return context.FlightsSchedules.Where(x=> x.UserName == userName && x.UniqueKey == uniqueKey).ToList();
            }
        }
    }
}
