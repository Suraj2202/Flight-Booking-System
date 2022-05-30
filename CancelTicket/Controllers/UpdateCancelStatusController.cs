using CancelTicket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CancelTicket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UpdateCancelStatusController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] BookedTicketDetails bookingId)
        {
            using(UserSideContext ctx = new UserSideContext())
            {
                BookedTicketDetails details = ctx.BookedTicketDetails?.Where(x => x.UniqueBookingId == bookingId.UniqueBookingId).FirstOrDefault();
                details.CanCancel = "False";
                details.CancelStatus = "Canceled";
                ctx.SaveChanges();
                return Ok();
            }
        }
    }
}
