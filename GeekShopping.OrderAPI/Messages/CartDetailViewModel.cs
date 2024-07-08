namespace GeekShopping.OrderAPI.Messages
{
    public class CartDetailViewModel 
    {
        public long Id { get; set; }
        public long CartHeaderId { get; set; }
        public long ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public int Count { get; set; }
    }
}
