using System.Linq.Expressions;

namespace PokemonReviewApp.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> RemoveRange(int[] ids);

        Task<bool> SaveAsync();
        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> ExistAsync(int id);
    }
}
