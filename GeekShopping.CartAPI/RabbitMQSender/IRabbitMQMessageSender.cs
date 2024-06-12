using GeekShopping.MessageBus;

namespace GeekShopping.CartAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMesssage, string queueName);
    }
}
