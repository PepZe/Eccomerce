using Ecommerce.Domain.Model;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        void Save();
    }
}
