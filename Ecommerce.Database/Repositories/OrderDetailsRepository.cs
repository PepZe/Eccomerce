using Ecommerce.Database.Context;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model.Order;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Database.Repositories
{
    public class OrderDetailsRepository : Repository<OrderDetail>, IOrderDetailsRepository
    {
        private ApplicationDbContext _dbContext;
        private readonly DbSet<OrderDetail> _ordersHeader;    
        public OrderDetailsRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
            _ordersHeader = _dbContext.OrderDetails;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(OrderDetail orderHeader)
        {
            _ordersHeader.Update(orderHeader);
        }
    }
}
