using GeekShopping.CartAPI.Repository;
using GeekShopping.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
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
    }
}
