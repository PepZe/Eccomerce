using Ecommerce.Database.Context;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model.Order;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Database.Repositories
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _dbContext;
        private readonly DbSet<OrderHeader> _ordersHeader;    
        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
            _ordersHeader = _dbContext.OrderHeaders;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(OrderHeader orderHeader)
        {
            _ordersHeader.Update(orderHeader);
        }
    }
}
