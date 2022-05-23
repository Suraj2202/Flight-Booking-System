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
            
            //save to data to DB

            using(UserSideContext ctx = new UserSideContext())
            {
                if (data.UniqueKey != null)
                {
                    FlightSchedules scheduleDetail = new FlightSchedules()
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
                        BaseFare = data?.BaseFare
                    };
                    
                    ctx.FlightSchedules.Add(scheduleDetail);
                    ctx.SaveChanges();
                }
                else if(data.ContactNumber != null)
                {
                    // TODO: 
                    // if Airline details are not in User DB, then activate Producer and catch/Save in DB
                }
                else if(data.FlightName != null)
                {
                    //TODO : save it in User Flight Table
                }
                
            }
        }
    }
}
