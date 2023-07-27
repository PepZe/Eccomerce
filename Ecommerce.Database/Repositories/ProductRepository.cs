using Ecommerce.Database.Context;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.Database.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly DbSet<Product> _productsSet;
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
            _productsSet = _db.Products;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            _productsSet.Update(product);
        }
    }
}
