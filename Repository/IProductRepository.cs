using TechLife.Models;

namespace TechLife.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        void Save();
    }
}
