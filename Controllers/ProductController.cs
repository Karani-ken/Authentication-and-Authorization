using Authentication_and_Authorization.Models;
using Authentication_and_Authorization.Request;
using Authentication_and_Authorization.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_and_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductInterface _productInterface;

        public ProductController(IProductInterface productInterface, IMapper mapper)
        {
            _mapper = mapper;
            _productInterface = productInterface;
        }
        [HttpPost]

        //Only Admons Allowed to Add product
        [Authorize]
        public async Task<ActionResult<string>> AddProducts(AddProduct newProduct)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role").Value;
            if (!string.IsNullOrWhiteSpace(role) && role == "Admin")
            {
                var product = _mapper.Map<Product>(newProduct);
                var res = await _productInterface.AddProductAsync(product);
                return CreatedAtAction(nameof(AddProducts), res);
            }
            return BadRequest("You are not Allowed to do that");
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productInterface.GetAllProducts();

            return Ok(products);
        }
    }
}
