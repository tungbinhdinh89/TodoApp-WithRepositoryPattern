using ToDoList.Application.Interfaces;
using ToDoList.Core.DB;

namespace Todo.Application.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    namespace Todo.Application.Repositories
    {
        public class ToDoRepository<TEntity> : ITodoRepository<TEntity>, IDisposable where TEntity : class
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly DbSet<TEntity> _dbSet;

            public ToDoRepository(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
                _dbSet = dbContext.Set<TEntity>();
            }

            public async Task<TEntity> GetByIdAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
            {
                IQueryable<TEntity> query = _dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                return await query.ToListAsync();
            }

            public async Task AddAsync(TEntity entity)
            {
                await _dbSet.AddAsync(entity);
            }

            public void Update(TEntity entity)
            {
                _dbSet.Update(entity);
            }

            public void Remove(TEntity entity)
            {
                _dbSet.Remove(entity);
            }

            public void Dispose()
            {
                _dbContext.Dispose();
            }
        }
    }

}
