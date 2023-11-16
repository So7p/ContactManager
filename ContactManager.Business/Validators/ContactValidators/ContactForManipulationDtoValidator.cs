using ContactManager.Business.DTOs.Contact;
using FluentValidation;

namespace ContactManager.Business.Validators.ContactValidators
{
    public class ContactForManipulationDtoValidator<T> : AbstractValidator<T> where T : ContactForManipulationDto
    {
        public ContactForManipulationDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Contact name cannot be null.");

            When(c => c.Name is not null, () =>
            {
                RuleFor(c => c.Name.Length)
                    .LessThan(31).WithMessage("The length of the contact name cannot be more than 30 characters."); 
            });


            RuleFor(c => c.MobilePhone)
                .NotNull().WithMessage("Phone number cannot be null.");

            When(c => c.MobilePhone is not null, () =>
            {
                RuleFor(c => c.MobilePhone)
                    .Matches("^\\+(?:[0-9]●?){6,14}[0-9]$").WithMessage("The entered value is not a phone number.");
            });


            RuleFor(c => c.JobTitle)
                .NotNull().WithMessage("Job title cannot be null.");

            When(c => c.JobTitle is not null, () =>
            {
                RuleFor(c => c.JobTitle.Length)
                    .LessThan(31).WithMessage("The length of the job title cannot be more than 30 characters.");
            });

            RuleFor(c => c.BirthDate)
                .NotNull().WithMessage("Date of birth cannot be null.")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-16)).WithMessage("Contact cannot be under 16 years of age.");
        }
    }
}