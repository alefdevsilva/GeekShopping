using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
using GeekShopping.CartAPI.Repository;
using GeekShopping.Data.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        private ICouponRepository _couponRepository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository cartRepository, 
            IRabbitMQMessageSender rabbitMQMessageSender, 
            ICouponRepository couponRepository)
        {
            _cartRepository = cartRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _couponRepository = couponRepository;
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartViewModel>> FindById(string id)
        {
            var cart = await _cartRepository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartViewModel>> AddCart(CartViewModel cartViewModel)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartViewModel);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartViewModel>> UpdateCart(CartViewModel cartViewModel)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartViewModel);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartViewModel>> RemoveCart(int id)
        {
            var status = await _cartRepository.RemoveFromcart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartViewModel>> ApplyCoupon(CartViewModel cartViewModel)
        {
            var status = await _cartRepository.ApplyCoupon(cartViewModel.CartHeader.UserId, cartViewModel.CartHeader.CouponCode);
            if (!status) return NotFound();
            return Ok(status);
        }
        
        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartViewModel>> RemoveCoupon(string userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO checkoutHeaderDTO)
        {
            var token = await HttpContext.GetTokenAsync("access_token"); ;
            if (checkoutHeaderDTO?.UserId == null) return BadRequest();

            var cart = await _cartRepository.FindCartByUserId(checkoutHeaderDTO.UserId);
            if (cart == null) return NotFound();

            if (!string.IsNullOrEmpty(checkoutHeaderDTO.CouponCode))
            {
                var coupon = await _couponRepository.GetCouponByCouponCode(checkoutHeaderDTO.CouponCode, token);
                if (checkoutHeaderDTO.DiscountAmount != coupon.DiscountAmount) 
                    return StatusCode(412);
            }
            checkoutHeaderDTO.CartDetails = cart.CartDetails;
            checkoutHeaderDTO.DateTime = DateTime.Now;

            _rabbitMQMessageSender.SendMessage(checkoutHeaderDTO, "checkoutqueue");

            await _cartRepository.ClearCart(checkoutHeaderDTO.UserId);

            return Ok(checkoutHeaderDTO);
        }
    }
}
