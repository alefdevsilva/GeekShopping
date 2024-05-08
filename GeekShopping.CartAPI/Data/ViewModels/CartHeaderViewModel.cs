using GeekShopping.CartAPI.Model.Base;

namespace GeekShopping.Data.ViewModels
{
    public class CartHeaderViewModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }

    }
}
