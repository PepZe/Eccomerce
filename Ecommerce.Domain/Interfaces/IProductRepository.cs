using Ecommerce.Domain.Model;

namespace Ecommerce.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Save();
        void Update(Product product);  
    }
}
