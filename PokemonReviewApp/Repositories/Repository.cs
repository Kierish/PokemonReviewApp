using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace PokemonReviewApp.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _context;
        public Repository(DataContext context) 
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().Where(expression).ToListAsync();     
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> RemoveRange(int[] ids)
        {
            var entitiesToDelete = await _context.Set<TEntity>()
                .Where(e => ids.Contains(EF.Property<int>(e, "Id")))
                .ToListAsync();
            
            if (entitiesToDelete == null || !entitiesToDelete.Any())
            {
                return false;
            }
            _context.Set<TEntity>().RemoveRange(entitiesToDelete);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return await SaveAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<TEntity>().AnyAsync(e => EF.Property<int>(e, "Id") == id);    
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await _context.Set<TEntity>().FindAsync(id);
            if (entityToDelete == null)
            {
                return false;
            }

            _context.Set<TEntity>().Remove(entityToDelete);
            return await SaveAsync();
        }
    }
}
