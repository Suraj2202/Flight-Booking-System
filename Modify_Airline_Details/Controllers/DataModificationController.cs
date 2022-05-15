using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Modify_Airline_Details.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataModificationController : ControllerBase
    {
        // GET: api/<DataModificationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Data Modification operations like Add, Block, cancel Airlines" };
        }

        // GET api/<DataModificationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DataModificationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DataModificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DataModificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
