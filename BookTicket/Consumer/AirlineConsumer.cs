using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared_Model;

namespace BookTicket.Consumer
{
    public class AirlineConsumer : IConsumer<SharedAirlineDetails>
    {
        public async Task Consume(ConsumeContext<SharedAirlineDetails> context)
        {
            var data = context.Message;

        }
    }
}
