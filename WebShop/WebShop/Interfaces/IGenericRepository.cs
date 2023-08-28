using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IQueryable<T>> GetAll();
        Task<T?> Get(int id);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
