using Ecommerce.Domain.Model.Order;

namespace Ecommerce.Domain.Interfaces
{
    public interface IOrderDetailsRepository : IRepository<OrderDetail>, IPersistData<OrderDetail>
    {
    }
}
