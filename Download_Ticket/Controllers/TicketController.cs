using Download_Ticket.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Download_Ticket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        // GET: api/<TicketController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Download Ticket" };
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public PostRequestedDetails Get(string id)
        {
            using(UserSideContext ctx = new UserSideContext())
            {
                List<BookedTicketDetails> alldata = ctx.BookedTicketDetails?.Where(x => x.Pnrnumber == id).ToList();
                string passangerName1 = "";
                string passangerAge1 = "";
                string seatNumber1 = "";
                string gender1 = "";
                string passangerName2 = "";
                string passangerAge2 = "";
                string seatNumber2 = "";
                string gender2 = "";

                if(alldata.Count > 1)
                {
                    passangerAge1 = alldata[1].PasangerAge;
                    passangerName1 = alldata[1].PasangerName;
                    gender1 = alldata[1].Gender;
                    seatNumber1 = alldata[1].SeatNumber;
                }
                if(alldata.Count > 2)
                {
                    passangerAge2 = alldata[2].PasangerAge;
                    passangerName2 = alldata[2].PasangerName;
                    gender2 = alldata[2].Gender;
                    seatNumber2 = alldata[2].SeatNumber;
                }

                return new PostRequestedDetails()
                {
                    UserName = alldata[0].UserName,
                    EmailId = alldata[0].EmailId,
                    PassangerName0 = alldata[0].PasangerName,
                    PassangerAge0 = alldata[0].PasangerAge,
                    Gender0 = alldata[0].Gender,
                    SeatNumber0 = alldata[0].SeatNumber,
                    PassangerName1 = passangerName1,
                    PassangerName2 = passangerName2,
                    PassangerAge1 = passangerAge1,
                    PassangerAge2 = passangerAge2,
                    Gender1 = gender1,
                    Gender2 = gender2,
                    SeatNumber1 = seatNumber1,
                    SeatNumber2 = seatNumber2                    
                };
            }
        }

        // POST api/<TicketController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
