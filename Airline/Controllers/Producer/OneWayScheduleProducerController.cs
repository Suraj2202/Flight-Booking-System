﻿using Airline.Models;
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
    public class OneWayScheduleProducerController : ControllerBase
    {
        private readonly IBusControl _bus;

        public OneWayScheduleProducerController(IBusControl bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleData([FromBody] RequestDetails requestDetails)
        {
            Uri uri = new Uri("rabbitmq://localhost/shared_airline_queue");
            using(InventoryContext context = new InventoryContext())
            {

                List<ScheduleDetails> allSchedule = context.ScheduleDetails
                                                            ?.Where<ScheduleDetails>(x => 
                                                            x.To == requestDetails.To && 
                                                            x.From == requestDetails.From)
                                                            .ToList();

                var endPoint = await _bus.GetSendEndpoint(uri);

                if(allSchedule != null)
                {
                    string uniqueValue = Guid.NewGuid().ToString();
                    foreach (var schedule in allSchedule)
                    {
                        if (GetDateTime(schedule.StartDateTime) >= GetDateTime(requestDetails.DepartStartDateTime))
                        {
                            double baseFare = (double)context.AirlineDetails.Where(x => x.FlightNumber == schedule.FlightNumber)
                                                                    .FirstOrDefault().BaseFare;

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
                                Meal = schedule.Meal,
                                BaseFare = baseFare
                            };
                            
                            await endPoint.Send(sharedScheduleDetails);
                        }
                    }
                    return Ok("Success " + uniqueValue);
                }
            }
            return BadRequest("Something Went Wrong");
        }

        private DateTime GetDateTime(string dateTime)
        {
            DateTime response = DateTime.ParseExact(dateTime, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return response;
        }
    }
}