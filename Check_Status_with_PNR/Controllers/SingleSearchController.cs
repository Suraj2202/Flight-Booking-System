using Check_Status_with_PNR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Check_Status_with_PNR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SingleSearchController : ControllerBase
    {

        [HttpGet("{pnr}")]
        public BookedTicketDetails Get(string pnr)
        {
            using(UserSideContext ctx = new UserSideContext())
            {
                return ctx.BookedTicketDetails?.Where<BookedTicketDetails>(x => x.Pnrnumber == pnr).FirstOrDefault();
            }
        }
    }
}
