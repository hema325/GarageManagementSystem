using GMS.API.Entities;

namespace GMS.API.Data.Repositories._BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<TEntity?> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
    }
}
