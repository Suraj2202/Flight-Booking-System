using System;
using System.Threading.Tasks;
using Airline.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Controllers.NewFolder
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController :
        ControllerBase
    {
        private readonly IBusControl _bus;

        public ValueController(IBusControl bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> AirlineData(AirlineDetails airlineDetails)
        {
            Uri uri = new Uri("rabbitmq://localhost/asdf");

            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(airlineDetails);

            return Ok("Success");
        }
    }
}

