using Authentication_and_Authorization.Models;

namespace Authentication_and_Authorization.Services.IServices
{
    public interface IProductInterface
    {
        Task<string> AddProductAsync(Product product);

        Task<IEnumerable<Product>> GetAllProducts();
    }
}
