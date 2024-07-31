using GeekShopping.MessageBus;
using GeekShopping.PaymentAPI.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private const string ExchangeName = "FanoutPaymentUpdateExchange";

        public RabbitMQMessageSender()
        {
            _hostName = "host.docker.internal";
            _port = 5672;
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage messsage)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                Port = _port,
                UserName = _userName,
                Password = _password,
            };

            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, durable: false);
            byte[] body = GetMessageAsByteArray(messsage);
            channel.BasicPublish(exchange: ExchangeName, "", basicProperties: null, body: body);
        }

        private byte[] GetMessageAsByteArray(BaseMessage messsage)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<UpdatePaymentResultMessage>((UpdatePaymentResultMessage)messsage, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
