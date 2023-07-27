namespace Ecommerce.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; set; }
        public IProductRepository Product { get; set; }
        void Save();
    }
}
