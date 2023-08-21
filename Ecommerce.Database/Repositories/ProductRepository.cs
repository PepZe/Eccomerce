using Ecommerce.Database.Context;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Microsoft.EntityFrameworkCore;

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
            var prodDto = _productsSet.FirstOrDefault(p => p.Id == product.Id);
            if(prodDto != null)
            {
                prodDto.Title = product.Title;
                prodDto.Description = product.Description;
                prodDto.Price = product.Price;
                prodDto.Price50 = product.Price50;
                prodDto.Price100 = product.Price100;
                prodDto.ISBN = product.ISBN;
                prodDto.ListPrice = product.ListPrice;
                prodDto.CategoryId = product.CategoryId;
                prodDto.Author = product.Author;
                if(product.ImageUrl != null)
                {
                    prodDto.ImageUrl = product.ImageUrl;
                }
            }

            Save();
        }
    }
}
