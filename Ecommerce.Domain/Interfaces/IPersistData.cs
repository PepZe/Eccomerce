using Ecommerce.Domain.Model;

namespace Ecommerce.Domain.Interfaces
{
    public interface IPersistData<T> where T : class
    {
        void Update(T obj);
        void Save();
    }
}
