using ContactManager.Data.Contexts.Contracts;
using ContactManager.Data.Entities;
using ContactManager.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Repositories.Implementation
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected IApplicationDbContext appContext;
        protected DbSet<TEntity> DbSet;

        public BaseRepository(IApplicationDbContext appContext)
        {
            this.appContext = appContext;
            DbSet = appContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await DbSet
            .AsNoTracking()
            .ToListAsync();

        public virtual async Task<TEntity?> GetByIdAsync(int id) =>
            await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var created = await DbSet.AddAsync(entity);
            await appContext.SaveChangesAsync();

            return created.Entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await appContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);

            await appContext.SaveChangesAsync();
        }
    }
}