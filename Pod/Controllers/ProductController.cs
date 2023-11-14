using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pod.Dto;
using Pod.IServices;
using Pod.Models.ServiceResponses;

namespace Pod.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _product;
        public ProductController(IProductService _product)
        {
            this._product = _product;
        }

        [HttpGet("GetAllProduct")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAllProduct()
        {
            var response = await _product.GetAllProduct();
            return Ok(response);
        }

        [HttpPost("AddProductToCart")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddProductToCart(int userId, int productId)
        {
            var response = await _product.AddProductToCart(userId, productId);
            return Ok(response);
        }

        [HttpDelete("DeleteProductFromCart")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProductToCart(int userId, int productId)
        {
            var response = await _product.DeleteProductToCart(userId, productId);
            return Ok(response);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddProduct(ProductFormDto productdto) // Здесь будешь добавлять продукты чтобы не ебаться с базой
        {
           await _product.AddProduct(productdto);
            return Ok();
        }

        [HttpGet("GetAllProductByCart")] // Вывод товаров в коризне у пользователя
        public async Task<ActionResult<ServiceResponseSum<List<ProductDto>>>> GetProductByCart(int userId)
        {
            var response = await _product.GetProductByCart(userId);
            return Ok(response);
        }
    }
}
