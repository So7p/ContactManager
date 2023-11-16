using ContactManager.Business.DTOs.Contact;
using ContactManager.Data.Entities;

namespace ContactManager.Business.MappingProfiles
{
    public class ContactMappingProfile : AutoMapper.Profile
    {
        public ContactMappingProfile() 
        {
            CreateMap<Contact, ContactForViewDto>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => ConvertToDateOnly(src.BirthDate)));

            CreateMap<ContactForCreationDto, Contact>();
            CreateMap<ContactForUpdateDto, Contact>();
        }

        private DateOnly ConvertToDateOnly(DateTime dateTime)
        {
            var year = dateTime.Year;
            var month = dateTime.Month;
            var day = dateTime.Day;

            DateOnly dateOnly = new DateOnly(year, month, day);

            return dateOnly;
        }
    }
}