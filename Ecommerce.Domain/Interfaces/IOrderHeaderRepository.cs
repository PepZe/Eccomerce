using Ecommerce.Domain.Model.Order;

namespace Ecommerce.Domain.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>, IPersistData<OrderHeader>
    {
    }
}
