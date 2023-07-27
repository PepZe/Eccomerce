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
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _productsSet = context.Products;
        }

        public void Add(Product entity)
        {
            _productsSet.Add(entity);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            return _productsSet.FirstOrDefault(filter);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productsSet.ToList();
        }

        public void Remove(Product entity)
        {
            _productsSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Product> entities)
        {
            _productsSet.RemoveRange(entities);
        }

        public void Update(Product product)
        {
            _productsSet.Update(product);
        }
    }
}
