namespace GeekShopping.Email.Repository
{
    public interface IOrderRepository
    {
        Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid);
    }
}
