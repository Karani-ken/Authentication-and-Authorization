using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Models;
using Authentication_and_Authorization.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Services
{
    public class ProductService : IProductInterface
    {
        private readonly AppDbContext _appDbContext;
        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;   
        }
        public async Task<string> AddProductAsync(Product product)
        {
            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();
            return "Product added";
         
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {

            return await _appDbContext.Products.ToListAsync();
        }
    }
}
