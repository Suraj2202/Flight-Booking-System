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
        public IEnumerable<FlightsDetails> Get()
        {
            using (InventoryContext context = new InventoryContext())
            {
                return context.FlightsDetails.ToList();
            }
        }

        // GET: Single Data
        [HttpGet("{flightsDetails}")]
        public IEnumerable<FlightsDetails> Get(string flightsDetails)
        {
            using (InventoryContext context = new InventoryContext())
            {
                return (IEnumerable<FlightsDetails>)context.FlightsDetails?.Where(x => x.FlightNumber == flightsDetails).FirstOrDefault();
            }
        }

        // POST api/<FlightDetailsController>
        [HttpPost]
        public void Post([FromBody] FlightsDetails value)
        {
            FlightsDetails flightDetails = new FlightsDetails();
            
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
                    context.FlightsDetails.Add(flightDetails);
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
