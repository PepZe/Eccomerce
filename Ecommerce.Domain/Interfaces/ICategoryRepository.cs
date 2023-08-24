using Ecommerce.Domain.Model;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICategoryRepository : IPersistData<Category>, IRepository<Category>
    {
    }
}
