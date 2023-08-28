using Ecommerce.Database.Context;
using Ecommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.Database.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private static readonly char[] separator = new char[] { ',' };

        public Repository(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        private IQueryable<T> AddOptionalsProperties(string optionalProperty, bool tracking)
        {
            IQueryable<T> query;
            if (tracking)
            {
                query = _dbSet.AsQueryable();
            }
            else
            {
                query = _dbSet.AsNoTracking();
            }

            if (!string.IsNullOrEmpty(optionalProperty))
            {
                foreach (var prop in optionalProperty.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            return query;
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracking = false)
        {
            var query = AddOptionalsProperties(includeProperties ?? string.Empty, tracking);
            return query.FirstOrDefault(filter);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracking = false)
        {
            var query = AddOptionalsProperties(includeProperties ?? string.Empty, tracking);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
