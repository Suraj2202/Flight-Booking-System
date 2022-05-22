using Airline.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared_Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Airline.Controllers.Producer
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoWayScheduleProducerController : ControllerBase
    {
        private readonly IBusControl _bus;

        public TwoWayScheduleProducerController(IBusControl bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleData([FromBody] RequestDetails requestDetails)
        {
            Uri uri = new Uri("rabbitmq://localhost/shared_airline_queue");
            using (InventoryContext context = new InventoryContext())
            {

                List<ScheduleDetails> allDepartSchedule = context.ScheduleDetails
                                                            ?.Where<ScheduleDetails>(x =>
                                                            x.To == requestDetails.To &&
                                                            x.From == requestDetails.From)
                                                            .ToList();
                
                List<ScheduleDetails> allReturnSchedule = context.ScheduleDetails
                                                                ?.Where<ScheduleDetails>(x =>
                                                                x.To == requestDetails.From &&
                                                                x.From == requestDetails.To)
                                                                .ToList();
                var endPoint = await _bus.GetSendEndpoint(uri);
                bool dataPosted = false;
                string uniqueValue = Guid.NewGuid().ToString();

                if (allDepartSchedule != null)
                {
                    foreach (var schedule in allDepartSchedule)
                    {
                        if (GetDateTime(schedule.StartDateTime) >= GetDateTime(requestDetails.DepartStartDateTime))
                        {
                            SharedAirlineDetails sharedScheduleDetails = new SharedAirlineDetails()
                            {
                                UserName = requestDetails.UserName,
                                UniqueKey = uniqueValue,
                                FlightNumber = schedule.FlightNumber,
                                ConfirmationNumber = schedule.ConfirmationNumber,
                                To = schedule.To,
                                From = schedule.From,
                                StartDateTime = schedule.StartDateTime,
                                EndDateTime = schedule.EndDateTime,
                                Schedule = schedule.Schedule,
                                Meal = schedule.Meal
                            };

                            await endPoint.Send(sharedScheduleDetails);
                            dataPosted = true;
                        }
                        else
                        {
                            return BadRequest("No Depart Data Available");
                        }
                    }
                }

                if(allReturnSchedule != null)
                {
                    foreach(var schedule in allReturnSchedule)
                    {
                        if (GetDateTime(schedule.StartDateTime) >= GetDateTime(requestDetails.ReturnStartDateTime))
                        {
                            SharedAirlineDetails sharedScheduleDetails = new SharedAirlineDetails()
                            {
                                UserName = requestDetails.UserName,
                                UniqueKey = uniqueValue,
                                FlightNumber = schedule.FlightNumber,
                                ConfirmationNumber = schedule.ConfirmationNumber,
                                To = schedule.To,
                                From = schedule.From,
                                StartDateTime = schedule.StartDateTime,
                                EndDateTime = schedule.EndDateTime,
                                Schedule = schedule.Schedule,
                                Meal = schedule.Meal
                            };

                            await endPoint.Send(sharedScheduleDetails);
                            dataPosted = true;
                        }
                        else
                        {
                            return BadRequest("No Return Data Available");
                        }
                    }
                }

                if (dataPosted)
                    return Ok("Success " + uniqueValue);
                else
                    return BadRequest("Something Went Wrong");
            }
        }

        private DateTime GetDateTime(string dateTime)
        {
            DateTime response = DateTime.ParseExact(dateTime, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return response;
        }
    }
}
