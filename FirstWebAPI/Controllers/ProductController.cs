using FirstWebAPI.Models;
using FirstWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    [ApiController]
    [Route("product")] // the URL path becomes => /product/...
    public class ProductController : ControllerBase
    {

        private ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetAllProducts")] // => /product/GetAllProducts
        public IActionResult GetAllProducts()
        {
            return Ok(productService.GetAllProducts());
        }

        [HttpGet("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            return Ok(productService.GetProductById(id));
        }

        [HttpPost("Add")]
        public IActionResult Add(Product product)
        {
            int productId = productService.Create(product);
            return Ok(new { ProductId = productId });
        }

        [HttpPut("UpdatePrice")]
        public IActionResult UpdatePrice(int productId, decimal newPrice)
        {
            bool updated = productService.UpdatePrice(productId, newPrice);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int productId)
        {
            bool deleted = productService.Delete(productId);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}