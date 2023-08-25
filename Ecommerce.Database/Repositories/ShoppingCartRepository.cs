using Ecommerce.Database.Context;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Database.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ShoppingCart> _dbSet;

        public ShoppingCartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.ShoppingCarts;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _dbSet.Update(shoppingCart);
        }
    }
}
