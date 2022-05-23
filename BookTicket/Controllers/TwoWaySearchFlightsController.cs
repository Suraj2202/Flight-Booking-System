using BookTicket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoWaySearchFlightsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<FlightSchedules> Get(FlightSchedules flight)
        {
            using (UserSideContext context = new UserSideContext())
            {
                return context.FlightSchedules.Where(x => x.UserName == flight.UserName && x.UniqueKey == flight.UniqueKey).ToList();
            }
        }
    }
}
