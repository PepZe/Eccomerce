using Ecommerce.Domain.Model;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICompanyRepository : IPersistData<Company>, IRepository<Company>
    {
    }
}
