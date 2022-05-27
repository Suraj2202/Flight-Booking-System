using BookTicket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneWayBookFlightController : ControllerBase
    {

        IBookingSystem _bookingSystem = new BookingSystem();

        [HttpPost]
        public IActionResult Post([FromBody] PostRequestedDetails requestedDetails)
        {
            if (requestedDetails != null)
            {
                
             
                using (UserSideContext ctx = new UserSideContext())
                {
                    FlightsSchedules flights = ctx.FlightsSchedules?.Where(x => x.UserName == requestedDetails.UserName &&
                                                                                x.EntryId == requestedDetails.SentOneUniqueId)
                                                                                .FirstOrDefault();

                    string pnrNumber = _bookingSystem.PNRNumberGeneration(flights.FlightNumber);

                    ctx.BookedTicketDetails.Add(ObjectCreation(requestedDetails,
                                                                flights,
                                                                Guid.NewGuid().ToString(), 
                                                                requestedDetails.PassangerName0, 
                                                                requestedDetails.PassangerAge0, 
                                                                requestedDetails.SeatNumber0, 
                                                                requestedDetails.Gender0, 
                                                                pnrNumber));

                    ctx.SaveChanges();

                    if (requestedDetails.PassangerAge1 != "")
                    {
                        ctx.BookedTicketDetails.Add(ObjectCreation(requestedDetails,
                                                                    flights,
                                                                    Guid.NewGuid().ToString(),
                                                                    requestedDetails.PassangerName1,
                                                                    requestedDetails.PassangerAge1,
                                                                    requestedDetails.SeatNumber1,
                                                                    requestedDetails.Gender1,
                                                                    pnrNumber));
                        ctx.SaveChanges();

                    }

                    if (requestedDetails.PassangerAge2 != "")
                    {
                        ctx.BookedTicketDetails.Add(ObjectCreation(requestedDetails,
                                                                    flights,
                                                                    Guid.NewGuid().ToString(),
                                                                    requestedDetails.PassangerName2,
                                                                    requestedDetails.PassangerAge2,
                                                                    requestedDetails.SeatNumber2,
                                                                    requestedDetails.Gender2,
                                                                    pnrNumber));
                        ctx.SaveChanges();

                    }

                    return Ok("Created PNR " + pnrNumber);
                }
                    
            }
            else
                return BadRequest("Booking Unsuccessfull");
        }
    
        private BookedTicketDetails ObjectCreation(PostRequestedDetails requestedDetails, FlightsSchedules flight, string uniqueKey, string name, string age, string seat, string gender, string pnr)
        {
            return new BookedTicketDetails()
            {
                UniqueBookingId = uniqueKey,
                UserName = requestedDetails.UserName,
                EmailId = requestedDetails.EmailId,
                FlightIid = flight.FlightNumber,
                FlightName = flight.FlightName,
                Pnrnumber = pnr,
                PasangerName = name,
                PasangerAge = age,
                SeatNumber = seat,
                Gender = gender,
                DepartureTime = flight.StartDateTime,
                DepartureFrom = flight.From,
                ArrivalTime = flight.EndDateTime,
                ArrivalTo = flight.To,
                Fare = flight.BaseFare
            };
        }
    
    }

}
