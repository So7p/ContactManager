using AutoMapper;
using ContactManager.Business.DTOs.Contact;
using ContactManager.Business.Exceptions;
using ContactManager.Business.Services.Contracts;
using ContactManager.Data.Entities;
using ContactManager.Data.Repositories.Contracts;

namespace ContactManager.Business.Services.Implementation
{
    public class ContactService : IContactService
    {
        private IMapper _mapper;
        private IContactRepository _contactRepository;

        public ContactService(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<IEnumerable<ContactForViewDto>> GetAllAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();

            var contactsViewModels = _mapper.Map<IEnumerable<ContactForViewDto>>(contacts);

            return contactsViewModels;
        }

        public async Task<ContactForViewDto?> GetByIdAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Contact was not found.");

            var contactViewModel = _mapper.Map<ContactForViewDto>(contact);

            return contactViewModel;
        }

        public async Task CreateAsync(ContactForCreationDto contactForCreationDto)
        {
            if (contactForCreationDto == null)
                throw new ArgumentNullException(nameof(contactForCreationDto));

            var contact = _mapper.Map<Contact>(contactForCreationDto);

            await _contactRepository.CreateAsync(contact);
        }

        public async Task UpdateAsync(ContactForUpdateDto contactForUpdateDto)
        {
            if (contactForUpdateDto == null)
                throw new ArgumentNullException(nameof(contactForUpdateDto));

            var existingContact = await _contactRepository.GetByIdAsync(contactForUpdateDto.Id)
                ?? throw new NotFoundException("Contact was not found.");

            var contact = _mapper.Map<Contact>(contactForUpdateDto);

            await _contactRepository.UpdateAsync(contact);
        }

        public async Task DeleteAsync(int id)
        {
            var existingContact = await _contactRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Contact was not found.");

            await _contactRepository.DeleteAsync(id);
        }
    }
}