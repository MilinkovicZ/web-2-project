using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly DbContext _dbContext;
        private DbSet<T> entities;
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<T?> Get(int id)
        {
            return await entities.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
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
