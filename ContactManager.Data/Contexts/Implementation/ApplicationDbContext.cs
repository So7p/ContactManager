using ContactManager.Data.Contexts.Contracts;
using ContactManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ContactManager.Data.Contexts.Implementation
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}