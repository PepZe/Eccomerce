using Ecommerce.Database.Context;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Database.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private DbSet<Company> _dbSet { get; set; }
        private ApplicationDbContext _dbContext { get; set; }
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Companies;
        }

        public void Save()
        {
            _dbContext.SaveChanges();   
        }

        public void Update(Company company)
        {
            _dbSet.Update(company);
        }
    }
}
