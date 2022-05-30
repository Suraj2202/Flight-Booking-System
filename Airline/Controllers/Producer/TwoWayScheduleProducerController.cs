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
    [Route("[controller]")]
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

                List<SchedulesDetails> allDepartSchedule = context.SchedulesDetails
                                                            ?.Where<SchedulesDetails>(x =>
                                                            x.To == requestDetails.To &&
                                                            x.From == requestDetails.From)
                                                            .ToList();
                
                List<SchedulesDetails> allReturnSchedule = context.SchedulesDetails
                                                                ?.Where<SchedulesDetails>(x =>
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
                        //Check for days of Entry
                        bool entrySuccess = false;
                        if (schedule.Schedule == "Daily")
                        {
                            entrySuccess = true;
                        }
                        else
                        {
                            string[] allDays = schedule.Schedule.Split(',');
                            foreach (string day in allDays)
                            {
                                if (GetDateTime(requestDetails.DepartStartDateTime).DayOfWeek.ToString() == day && !entrySuccess)
                                {
                                    entrySuccess = true;
                                }
                            }
                        }

                        if (GetDateTime(schedule.StartDateTime) >= GetDateTime(requestDetails.DepartStartDateTime) && entrySuccess)
                        {
                            string flightName = context.FlightsDetails.Where(x => x.FlightNumber == schedule.FlightNumber)
                                                                         .FirstOrDefault().FlightName;

                            AirlinesDetails airline = context.AirlinesDetails?.Where(x => x.FlightNumber == schedule.FlightNumber)
                                                                               .FirstOrDefault();

                            if (airline.Blocked != null && airline.Blocked != "1")
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
                                    StartDateTime = GetDateTime(requestDetails.DepartStartDateTime).AddHours(r.Next(4, 15)).ToString(),
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
                                dataPosted = true;
                            }
                        }
                    }
                }

                if(allReturnSchedule != null)
                {
                    foreach(var schedule in allReturnSchedule)
                    {
                        //Check for days of Entry
                        bool entrySuccess = false;
                        if (schedule.Schedule == "Daily")
                        {
                            entrySuccess = true;
                        }
                        else
                        {
                            string[] allDays = schedule.Schedule.Split(',');
                            foreach (string day in allDays)
                            {
                                if (GetDateTime(requestDetails.DepartStartDateTime).DayOfWeek.ToString() == day && !entrySuccess)
                                {
                                    entrySuccess = true;
                                }
                            }
                        }

                        if (GetDateTime(schedule.StartDateTime) >= GetDateTime(requestDetails.ReturnStartDateTime) && entrySuccess)
                        {
                            string flightName = context.FlightsDetails.Where(x => x.FlightNumber == schedule.FlightNumber)
                                                                         .FirstOrDefault().FlightName;

                            AirlinesDetails airline = context.AirlinesDetails?.Where(x => x.FlightNumber == schedule.FlightNumber)
                                                                               .FirstOrDefault();

                            if(airline.Blocked!=null && airline.Blocked != "1")
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
                                    StartDateTime = GetDateTime(requestDetails.ReturnStartDateTime).AddHours(r.Next(4, 15)).ToString(),
                                    EndDateTime = GetDateTime(requestDetails.ReturnStartDateTime).AddHours(5).ToString(),
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
                                dataPosted = true;
                        }
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
            string[] newDate = dateTime.Split(' ')[0].Split('-');
            string newTime = "";
            if (dateTime.Split(' ').Length > 1)
                newTime = " " + dateTime.Split(' ')[1];
            else
                newTime = " 00:00:00";

            DateTime response = DateTime.ParseExact(newDate[1] + "/" + newDate[2] + "/" + newDate[0] + newTime, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return response;
        }
    }
}
