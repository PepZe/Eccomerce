using Ecommerce.Domain.Model;

namespace Ecommerce.Domain.Interfaces
{
    public interface IAplicationUserRepository : IRepository<ApplicationUser>
    {
        void Save();
    }
}
