using Microsoft.AspNetCore.Mvc;
using Modify_Airline_Details.Models;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Modify_Airline_Details.Controllers.Airline
{
    [Route("[controller]")]
    [ApiController]
    public class UpdateAirlineDetailsController : ControllerBase
    {
        
        // POST api/<UpdateAirlineDetailsController>
        [HttpPost("{checkValue}")]
        public IActionResult Post(string checkValue,[FromBody] AirlinesDetails airlineDetails)
        {
            using (InventoryContext context = new InventoryContext())
            {
                AirlinesDetails airline = context.AirlinesDetails.Where(x => x.FlightNumber == checkValue).FirstOrDefault();
                if (airline != null)
                {
                    if (airlineDetails.ContactNumber != null)
                        airline.ContactNumber = airlineDetails.ContactNumber;
                    if (airlineDetails.ContactAddress != null)
                        airline.ContactAddress = airlineDetails.ContactAddress;
                    if (airlineDetails.InstrumentUsed != null)
                        airline.InstrumentUsed = airlineDetails.InstrumentUsed;
                    if (airlineDetails.BusinessSeats != null)
                        airline.BusinessSeats = airlineDetails.BusinessSeats;
                    if (airlineDetails.NonBusinessSeats != null)
                        airline.NonBusinessSeats = airlineDetails.NonBusinessSeats;
                    if (airlineDetails.BaseFare != null)
                        airline.BaseFare = airlineDetails.BaseFare;
                    if (airlineDetails.BusinessRows != null)
                        airline.BusinessRows = airlineDetails.BusinessRows;
                    if (airlineDetails.NonBusinessRows != null)
                        airline.NonBusinessRows = airlineDetails.NonBusinessRows;
                    if(airline.Blocked != null)
                        airline.Blocked = airlineDetails.Blocked;
                    

                    // update all the values passed from 
                    context.SaveChanges();
                    return Ok("Success");
                }
                else
                    return BadRequest();
            }
        }

    }
}
