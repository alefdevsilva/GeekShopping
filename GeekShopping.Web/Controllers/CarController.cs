using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CarController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> CartIndex()
        {
            return View(await FindUserCart());
        }

        private async Task<CartViewModel> FindUserCart()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartService.FindCartByUserId(UserId, token);
            if (response?.CartHeader != null)
            {
                foreach (var detail in response.CartDetails)
                {
                    response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                }
            }
            return response;
        }
    }
}
