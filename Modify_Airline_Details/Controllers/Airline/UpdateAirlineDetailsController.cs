using Microsoft.AspNetCore.Mvc;
using Modify_Airline_Details.Models;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Modify_Airline_Details.Controllers.Airline
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateAirlineDetailsController : ControllerBase
    {
        
        // POST api/<UpdateAirlineDetailsController>
        [HttpPost]
        public void Post([FromBody] AirlineDetails airlineDetails)
        {
            using (InventoryContext context = new InventoryContext())
            {
                AirlineDetails airline = context.AirlineDetails.Where(x => x.FlightNumber == airlineDetails.FlightNumber).FirstOrDefault();
                if (airlineDetails.FlightNumber != null && airline != null)
                {
                    if (airlineDetails.Logo != null)
                        airline.Logo = airlineDetails.Logo;
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

                    // update all the values passed from 
                    context.SaveChanges();
                }
                else
                    return;
            }
        }

    }
}
