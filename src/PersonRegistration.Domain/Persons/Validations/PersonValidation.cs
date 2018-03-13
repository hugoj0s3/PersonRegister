using FluentValidation;

namespace PersonRegistration.Domain.Persons.Validations
{
    public class PersonValidation : AbstractValidator<Person>
    {
        public PersonValidation()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is invalid");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithMessage("FirstName is invalid");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithMessage("FirstName is invalid");
        }
    }
}
