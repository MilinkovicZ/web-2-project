using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly DbContext dbContext;
        private DbSet<T> entities;
        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            entities = dbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task Insert(T entity)
        {
            await entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }
        public void Delete(T entity)
        {
            //entities.Remove(entity);
            entity.IsDeleted = true;
        }
    }
}
