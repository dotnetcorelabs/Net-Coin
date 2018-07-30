using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoin.Communication.RabbitMQ
{
    public delegate void ConsumerReceived(object sender, BasicDeliverEventArgs e);

    
}
