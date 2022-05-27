using Check_Status_with_PNR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Check_Status_with_PNR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllTicketsController : ControllerBase
    {
        [HttpGet("{uname}")]
        public IEnumerable<BookedTicketDetails> Get(string uname)
        {
            using (UserSideContext ctx = new UserSideContext())
            {
                List<BookedTicketDetails> details = ctx.BookedTicketDetails?.Where(x => x.UserName == uname).ToList();
                List<string> bookingId = new List<string>();
                foreach(BookedTicketDetails detail in details)
                {
                    if(GetDateTime(detail.DepartureTime).AddHours(24) > DateTime.Now)
                    {
                        bookingId.Add(detail.UniqueBookingId);
                    }
                }


                foreach(string id in bookingId)
                {
                    BookedTicketDetails update = ctx.BookedTicketDetails?.Where(x => x.UniqueBookingId == id).FirstOrDefault();
                    update.CanCancel = "True";
                    ctx.SaveChanges();
                }


                return ctx.BookedTicketDetails?.Where(x=>x.UserName == uname).OrderBy(x=>x.DepartureTime).ToList();
            }
        }

        private DateTime GetDateTime(string dateTime)
        {
            string[] newDate = dateTime.Split(' ')[0].Split('-');
            string newTime = "";
            if (dateTime.Split(' ').Length > 1)
                newTime = " " + dateTime.Split(' ')[1];
            else
                newTime = " 00:00:00";

            DateTime response = DateTime.ParseExact(newDate[1] + "/" + newDate[0] + "/" + newDate[2] + newTime, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return response;
        }
    }
}
