using Airline.FieldSetting;
using Airline.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airline.Controllers.Airlines
{
    [Route("[controller]")]
    [ApiController]
    public class AirlineDetailsController : ControllerBase
    {
        #region//private variables
        private IAirlineData airline = new AirlineData();
        #endregion

        // GET: api/<AirlineDetailsController>
        [HttpGet]
        public IEnumerable<AirlinesDetails> Get()
        {
            using (InventoryContext context = new InventoryContext())
            {
                return context.AirlinesDetails.ToList();
            }
        }

        //Get Particular data
        [HttpGet("{value}")]
        public AirlinesDetails Get(string value)
        {
            using (InventoryContext context = new InventoryContext())
            {
                return (context.AirlinesDetails?.Where<AirlinesDetails>(x => x.FlightNumber == value).FirstOrDefault());
            }
        }

        // POST api/<AirlineDetailsController>
        [HttpPost]
        public IActionResult PostAirline([FromBody] AirlinesDetails value)
        {
            //Previous Condition : first check if Flight nUmber is already in FlightDetails Table or not
            if (airline.IsFlightNumberAvailable(value.FlightNumber))
            {                
                AirlinesDetails airlineDetails = new AirlinesDetails()
                {
                    FlightNumber = value.FlightNumber,
                    Logo = airline.GetAirlineLogoPath(value.FlightNumber),
                    ContactNumber = value.ContactNumber,
                    ContactAddress = value.ContactAddress,
                    InstrumentUsed = value.InstrumentUsed,
                    BusinessSeats = value.BusinessSeats,
                    NonBusinessSeats = value.NonBusinessSeats,
                    BaseFare = value.BaseFare,
                    BusinessRows = value.BusinessRows,
                    NonBusinessRows = value.NonBusinessRows,    
                    Blocked = "0"
                };


                //adding to Airline Details DB
                using (InventoryContext context = new InventoryContext())
                {
                    context.AirlinesDetails.Add(airlineDetails);

                    context.SaveChanges();
                    //Once Details is updated, Update the DetailsUpdated as 1
                    FlightsDetails flightDetails = context.FlightsDetails.Where(flight => flight.FlightNumber == airlineDetails.FlightNumber).FirstOrDefault();
                    flightDetails.DetailsUpdated = "1";

                    context.SaveChanges();
                    return Ok("Success");
                }
            }
            return BadRequest();
        }

    }
}
