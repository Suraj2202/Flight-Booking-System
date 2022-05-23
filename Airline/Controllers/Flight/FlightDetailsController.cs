using Airline.FieldSetting;
using Airline.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace Airline.Controllers.Flight
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightDetailsController : ControllerBase
    {
        #region
        //private variable
        private IAirlineData airlineData = new AirlineData();
        #endregion

        // GET: api/<FlightDetailsController>
        [HttpGet]
        public IEnumerable<FlightDetails> Get()
        {
            using (InventoryContext context = new InventoryContext())
            {
                return context.FlightDetails.ToList();
            }
        }

        // POST api/<FlightDetailsController>
        [HttpPost]
        public void Post([FromBody] FlightDetails value)
        {
            FlightDetails flightDetails = new FlightDetails();
            
            if(airlineData.IsFlightNumberAvailable(value.FlightNumber))
            {
                flightDetails.FlightNumber = value.FlightNumber;
                flightDetails.FlightName = airlineData.GetFlightName(value.FlightNumber);
            }
            else if(value.FlightName != null && value.FlightNumber != null)
            {
                flightDetails.FlightNumber = value.FlightNumber;
                flightDetails.FlightName = value.FlightName;
            }

            if(flightDetails != null)
            {
                using (InventoryContext context = new InventoryContext())
                {
                    context.FlightDetails.Add(flightDetails);
                    context.SaveChanges();
                }
            }
                

        }

        // PUT api/<AddFlightDetailsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddFlightDetailsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
