using NetCoin.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace NetCoin.Communication.RabbitMQ
{
    public abstract class RabbitBase : IDisposable
    {
        protected string HostName { get; }
        protected string QueueName { get; }

        protected readonly ConnectionFactory _factory;

        protected IConnection _connection;
        protected IModel _channel;

        private readonly bool _durable;
        private bool _queueDeclared = false;

        public event EventHandler<MessageEventArgs> OnReceiveMessage;

        public RabbitBase(string hostname, string queueName, bool durable)
        {
            HostName = hostname;
            QueueName = queueName;
            _durable = durable;

            _factory = new ConnectionFactory() { HostName = hostname };
        }

        public void Initialize()
        {
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        public void Listen()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += Consumer_Received1;

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
        }

        private void Consumer_Received1(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body);

            OnReceiveMessage?.Invoke(this, new MessageEventArgs(message));

            _channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
        }

        public void Send(string message)
        {
            if (_queueDeclared)
                _channel.QueueDeclare(queue: QueueName, durable: _durable, exclusive: false, autoDelete: false, arguments: null);

            byte[] body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: properties, body: body);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_channel.IsOpen)
                        _channel.Close();
                    _channel.Dispose();

                    if (_connection.IsOpen)
                        _connection.Close();

                    _connection.Dispose();
                }

                disposedValue = true;
            }
        }

        ~RabbitBase()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
