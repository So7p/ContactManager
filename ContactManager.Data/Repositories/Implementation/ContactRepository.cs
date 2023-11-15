using ContactManager.Data.Contexts.Contracts;
using ContactManager.Data.Entities;
using ContactManager.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Repositories.Implementation
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(IApplicationDbContext appContext) : base(appContext)
        {
        }

        public override async Task<IEnumerable<Contact>> GetAllAsync() =>
            await appContext.Set<Contact>()
            .AsNoTracking()
            .ToListAsync();

        public override async Task<Contact?> GetByIdAsync(int id) =>
            await appContext.Set<Contact>()
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}