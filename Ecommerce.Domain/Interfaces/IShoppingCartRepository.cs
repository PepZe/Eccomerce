using Ecommerce.Domain.Model;

namespace Ecommerce.Domain.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>, IPersistData<ShoppingCart>
    {

    }
}
