using GeekShopping.Data.ViewModels;

namespace GeekShopping.CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartViewModel> FindCartByUserId(string userId);
        Task<CartViewModel> SaveOrUpdateCart(CartViewModel cartViewModel);
        Task<bool> RemoveFromcart(long cartDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}
