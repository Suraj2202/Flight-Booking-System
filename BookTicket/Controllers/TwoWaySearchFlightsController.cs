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
        public IEnumerable<FlightsSchedules> Get(FlightsSchedules flight)
        {
            using (UserSideContext context = new UserSideContext())
            {
                return context.FlightsSchedules.Where(x => x.UserName == flight.UserName && x.UniqueKey == flight.UniqueKey).ToList();
            }
        }
    }
}
