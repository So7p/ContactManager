using ContactManager.Business.DTOs.Contact;

namespace ContactManager.Business.Services.Contracts
{
    public interface IContactService
    {
        Task<IEnumerable<ContactForViewDto>> GetAllAsync();
        Task<ContactForViewDto?> GetByIdAsync(int id);
        Task CreateAsync(ContactForCreationDto contactForCreationDto);
        Task UpdateAsync(ContactForUpdateDto contactForUpdateDto);
        Task DeleteAsync(int id);
    }
}