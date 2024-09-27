using GMS.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GMS.API.Data.Repositories._BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual void Create(TEntity entity)
            => _context.Set<TEntity>().Add(entity);

        public virtual void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public virtual void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public virtual async Task<TEntity?> GetByIdAsync(int id)
            => await _context.Set<TEntity>().FindAsync(id);

        public virtual async Task<List<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();
    }
}
