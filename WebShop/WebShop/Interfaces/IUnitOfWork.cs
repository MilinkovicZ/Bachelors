using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UsersRepository { get; }
        IGenericRepository<Product> ProductsRepository { get; }
        IGenericRepository<Item> ItemsRepository { get;}
        IGenericRepository<Order> OrdersRepository { get; }
        Task Save();
    }
}
