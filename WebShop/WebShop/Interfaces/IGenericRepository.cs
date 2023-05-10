using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
