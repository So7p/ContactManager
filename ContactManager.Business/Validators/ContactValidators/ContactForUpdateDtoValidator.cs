using ContactManager.Business.DTOs.Contact;
using FluentValidation;

namespace ContactManager.Business.Validators.ContactValidators
{
    public class ContactForUpdateDtoValidator : ContactForManipulationDtoValidator<ContactForUpdateDto>
    {
        public ContactForUpdateDtoValidator()
        {
            RuleFor(c => c.Id)
                .NotNull().WithMessage("Id cannot be null.");
        }
    }
}