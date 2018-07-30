using NetCoin.Common.Logger;
using RabbitMQ.Client;
using System;
using System.Text;

namespace NetCoin.Communication.RabbitMQ
{
    public class RabbitClient : RabbitBase
    {
        public RabbitClient(string hostname, string queueName) : base(hostname, queueName, true)
        {
        }
    }
}
