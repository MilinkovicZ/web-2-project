using Microsoft.EntityFrameworkCore;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;
        public IGenericRepository<User> UsersRepository { get; }
        public IGenericRepository<Product> ProductsRepository { get; }
        public IGenericRepository<Item> ItemsRepository { get; }
        public IGenericRepository<Order> OrdersRepository { get; }

        public UnitOfWork(DbContext dbContext, IGenericRepository<User> userRepository, IGenericRepository<Product> productRepository,
            IGenericRepository<Item> itemRepository, IGenericRepository<Order> orderRepository)
        {
            this.dbContext = dbContext;
            UsersRepository = userRepository;
            ProductsRepository = productRepository;
            ItemsRepository = itemRepository;
            OrdersRepository = orderRepository;
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
    }
}
