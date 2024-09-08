
using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.RabbitMQSender;
using GeekShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _orderRepository;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(OrderRepository orderRepository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _orderRepository = orderRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672

            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var checkoutHeaderViewModel = JsonSerializer.Deserialize<CheckoutHeaderViewModel>(content);
                ProcessOrder(checkoutHeaderViewModel).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume("checkoutqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderViewModel checkoutHeaderViewModel)
        {
            OrderHeader orderHeader = new()
            {
                UserId = checkoutHeaderViewModel.UserId,
                FirstName = checkoutHeaderViewModel.FirstName,
                LastName = checkoutHeaderViewModel.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = checkoutHeaderViewModel.CardNumber,
                CouponCode = checkoutHeaderViewModel.CouponCode,
                CVV = checkoutHeaderViewModel.CVV,
                DiscountAmount = checkoutHeaderViewModel.DiscountAmount,
                Email = checkoutHeaderViewModel.Email,
                ExpiryMonthYear = checkoutHeaderViewModel.ExpiryMonthYear += "Teste",
                OrderTime = DateTime.Now,
                PurchaseAmount = checkoutHeaderViewModel.PurchaseAmount,
                PaymentStatus = false,
                Phone = checkoutHeaderViewModel.Phone,
                PurchaseDate = checkoutHeaderViewModel.DateTime,

            };

            foreach (var details in checkoutHeaderViewModel.CartDetails)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = details.ProductId,
                    ProductName = details.Product.Name,
                    Price = details.Product.Price,
                    Count = details.Count
                };

                orderHeader.OrderTotalItens += details.Count;
                orderHeader.OrderDetails.Add(orderDetail);
            }

            await _orderRepository.AddOrder(orderHeader);

            PaymentViewModel payment = new()
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear + " ",
                OrderId = orderHeader.Id,
                PurchaseAmount = orderHeader.PurchaseAmount,
                Email = orderHeader.Email
            };

            try
            {
                _rabbitMQMessageSender.SendMessage(payment, "orderpaymentprocessqueue");
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }
    }
}
