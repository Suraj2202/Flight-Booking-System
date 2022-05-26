using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared_Model;
using Newtonsoft.Json;
using BookTicket.Models;

namespace BookTicket.Consumer
{
    public class AirlineConsumer : IConsumer<SharedAirlineDetails>
    {
        public async Task Consume(ConsumeContext<SharedAirlineDetails> context)
        {
            var data = context.Message;
            Console.WriteLine(data);
            //save to data to DB

            using(UserSideContext ctx = new UserSideContext())
            {
                if (data.UniqueKey != null)
                {
                    FlightsSchedules scheduleDetail = new FlightsSchedules()
                    {
                        EntryId = Guid.NewGuid().ToString(),
                        FlightNumber = data.FlightNumber,
                        ConfirmationNumber = data.ConfirmationNumber,
                        From = data.From,
                        To = data.To,
                        StartDateTime = data.StartDateTime,
                        EndDateTime = data.EndDateTime,
                        Schedule = data.Schedule,
                        Meal = data.Meal,
                        UserName = data.UserName,
                        UniqueKey = data.UniqueKey,
                        FlightName = data.FlightName,
                        ContactNumber = data.ContactNumber,
                        ContactAddress = data.ContactAddress,
                        InstrumentUsed = data.InstrumentUsed,
                        BusinessRows = data.BusinessRows,
                        NonBusinessRows = data.NonBusinessRows,
                        BaseFare = data.BaseFare,
                        BusinessSeats = data.BusinessSeats,
                        NonBusinessSeats = data.NonBusinessSeats

                    };
                    
                    ctx.FlightsSchedules.Add(scheduleDetail);
                    ctx.SaveChanges();
                }                                
            }
        }
    }
}
