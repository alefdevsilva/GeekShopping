using AutoMapper;
using GeekShopping.CartAPI.Model.Context;
using GeekShopping.Data.ViewModels;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly SQLServerContext _context;
        private IMapper _mapper;

        public CartRepository(SQLServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> FindCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromcart(long cartDetailsId)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> SaveOrUpdateCart(CartViewModel cartViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
