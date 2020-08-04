using ProductApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApi.Repository
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        Task<IEnumerable<Product>> ListProducts();
        Task<Product> GetProduct(int id);
        Task<bool> SaveChangesAsync();
    }
}
