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

                List<SchedulesDetails> allSchedule = context.SchedulesDetails
                                                            ?.Where<SchedulesDetails>(x => 
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
                            string flightName = context.FlightsDetails.Where(x => x.FlightNumber == schedule.FlightNumber)
                                                                        .FirstOrDefault().FlightName;

                            AirlinesDetails airline = context.AirlinesDetails?.Where(x => x.FlightNumber == schedule.FlightNumber)
                                                                               .FirstOrDefault();

                            //Check for days of Entry
                            bool entrySuccess = false;
                            if(schedule.Schedule == "Daily")
                            {
                                entrySuccess = true;
                            }
                            else
                            {
                                string[] allDays = schedule.Schedule.Split(',');
                                foreach(string day in allDays)
                                {
                                    if(GetDateTime(requestDetails.DepartStartDateTime).DayOfWeek.ToString() == day)
                                    {
                                        entrySuccess = true;
                                    }
                                }
                            }


                            if (airline.Blocked != null && airline.Blocked != "1" && entrySuccess)
                            {
                                Random r = new Random();
                                SharedAirlineDetails sharedScheduleDetails = new SharedAirlineDetails()
                                {
                                    UserName = requestDetails.UserName,
                                    UniqueKey = uniqueValue,
                                    FlightNumber = schedule.FlightNumber,
                                    ConfirmationNumber = schedule.ConfirmationNumber,
                                    To = schedule.To,
                                    From = schedule.From,
                                    StartDateTime = GetDateTime(requestDetails.DepartStartDateTime).AddHours(r.Next(4,15)).ToString(),
                                    EndDateTime = GetDateTime(requestDetails.DepartStartDateTime).AddHours(5).ToString(),
                                    Schedule = schedule.Schedule,
                                    Meal = schedule.Meal,

                                    //Flight Name
                                    FlightName = flightName,

                                    //Airline Details
                                    ContactNumber = airline.ContactNumber,
                                    ContactAddress = airline.ContactAddress,
                                    InstrumentUsed = airline.InstrumentUsed,
                                    BusinessRows = airline.BusinessRows,
                                    NonBusinessRows = airline.NonBusinessRows,
                                    BaseFare = airline.BaseFare,
                                    BusinessSeats = airline.BusinessSeats,
                                    NonBusinessSeats = airline.NonBusinessSeats,
                                    ClassOption = requestDetails.ClassOption


                                };

                                await endPoint.Send(sharedScheduleDetails);

                            }


                        }
                    }
                    return Ok("Success " + uniqueValue);
                }
            }
            return BadRequest("Something Went Wrong");
        }

        private DateTime GetDateTime(string dateTime)
        {
            string[] newDate = dateTime.Split(' ')[0].Split('-');
            string newTime = "";
            if (dateTime.Split(' ').Length > 1)
                newTime = " " + dateTime.Split(' ')[1];
            else
                newTime = " 00:00:00";

            DateTime response = DateTime.ParseExact(newDate[1]+"/"+newDate[2]+"/"+newDate[0]+newTime, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return response;
        }

 


    }
}
