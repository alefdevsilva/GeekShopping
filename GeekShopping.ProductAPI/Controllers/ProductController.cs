using GeekShopping.ProductAPI.Data.DTOs;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new
                ArgumentNullException(nameof(productRepository));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> FindAll()
        {
            var products = await _productRepository.FindAll();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null) return BadRequest();
            var product = await _productRepository.Create(productDTO);
            return Ok(product);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null) return BadRequest();
            var product = await _productRepository.Update(productDTO);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _productRepository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}
