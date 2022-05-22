using System;
using System.Threading.Tasks;
using Airline.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Shared_Model;

namespace Airline.Controllers.Producer
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineProducerController :
        ControllerBase
    {
        private readonly IBusControl _bus;

        public AirlineProducerController(IBusControl bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> AirlineData([FromBody] AirlineDetails airlineFlight)
        {
            Uri uri = new Uri("rabbitmq://localhost/airlines_queue");
            using (InventoryContext context = new InventoryContext())
            {
                AirlineDetails airlineDetails = context.AirlineDetails?.Where(x => x.FlightNumber == airlineFlight.FlightNumber).FirstOrDefault();
                var endPoint = await _bus.GetSendEndpoint(uri);
                if (airlineDetails != null)
                {
                    SharedAirlineDetails sharedAirlineDetails = new SharedAirlineDetails()
                    {
                        FlightNumber = airlineDetails.FlightNumber,
                        Logo = airlineDetails.Logo,
                        ContactNumber = airlineDetails.ContactNumber,
                        ContactAddress = airlineDetails.ContactAddress,
                        InstrumentUsed = airlineDetails.InstrumentUsed,
                        BusinessSeats = airlineDetails.BusinessSeats,
                        NonBusinessSeats = airlineDetails.NonBusinessSeats,
                        BaseFare = airlineDetails.BaseFare,
                        BusinessRows = airlineDetails.BusinessRows,
                        NonBusinessRows = airlineDetails.NonBusinessRows,
                        Blocked = airlineDetails.Blocked
                    };
                    await endPoint.Send(sharedAirlineDetails);

                    return Ok("Success");
                }
            }
            return BadRequest("Something Went Wrong");
        }
    }
}

