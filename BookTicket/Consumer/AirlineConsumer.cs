using BookTickets.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTicket.Consumer
{
    public class AirlineConsumer : IConsumer<AirlineDetails>
    {
        public async Task Consume(ConsumeContext<AirlineDetails> context)
        {
            var data = context.Message;
        }
    }
}
