using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CancelTicket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SecondController : ControllerBase
    {
        // GET: api/<SecondController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"Check History & ", "Cancel Ticket" };
        }

        // GET api/<SecondController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SecondController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SecondController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SecondController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
