using ContactManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Contexts.Contracts
{
    public interface IApplicationDbContext
    {
        DbSet<Contact> Contacts { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}