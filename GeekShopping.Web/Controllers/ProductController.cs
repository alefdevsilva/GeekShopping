using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts();
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductViewModel productViewModel)
        {
            if(ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(productViewModel);
                if (response != null) return RedirectToAction(nameof(ProductIndex)); 
            }
            return View(productViewModel);
        } 
        
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var viewModel = await _productService.FindProductById(id);
            if(viewModel != null) return View(viewModel);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductViewModel productViewModel)
        {
            if(ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(productViewModel);
                if (response != null) return RedirectToAction(nameof(ProductIndex)); 
            }
            return View(productViewModel);
        }
    }
}
