using System.Collections.ObjectModel;
using PersonRegistration.Domain.Core;
using PersonRegistration.Domain.Extensions;
using PersonRegistration.Domain.Persons.Validations;

namespace PersonRegistration.Domain.Persons
{
    public class Person : Entity
    {
        private static readonly PersonValidation PersonValidation = new PersonValidation();

        public Person(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            this.Errors = new ReadOnlyCollection<DomainNotification>(PersonValidation.Validate(this).ToDomainNotification());
        }

        // EF constructor
        private Person()
        {

        }
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public void ChangeName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

           this.Errors = new ReadOnlyCollection<DomainNotification>(PersonValidation.Validate(this).ToDomainNotification());
        }

        public void ChangeEmail(string email)
        {
            Email = email;
            this.Errors = new ReadOnlyCollection<DomainNotification>(PersonValidation.Validate(this).ToDomainNotification());
        }
    }
}
