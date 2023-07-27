namespace Ecommerce.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; set; }
        void Save();
    }
}
