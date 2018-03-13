using System;
using System.Collections.Generic;
using PersonRegistration.Domain.Core;
using PersonRegistration.Domain.Persons.Repository;

namespace PersonRegistration.Domain.Persons.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly NotificationContainer _notificationContainer;

        public PersonService(IPersonRepository repository, NotificationContainer notificationContainer)
        {
            _repository = repository;
            _notificationContainer = notificationContainer;
        }

        public Person GetPerson(Guid personId)
        {
            return _repository.GetPerson(personId);
        }

        public IList<Person> SearchPersons(string text)
        {
            return _repository.SearchPersons(text);
        }

        public Guid Insert(string firstName, string lastName, string email)
        {
            var person = new Person(firstName, lastName, email);
            if (!person.IsValid)
            {
                _notificationContainer.AddRange(person.Errors);
                return Guid.Empty;
            }

            _repository.Insert(person);
            return person.Id;
        }

        public void Delete(Guid personId)
        {
            var deleted = _repository.Delete(personId);

            if (!deleted)
            {
                _notificationContainer.Add("Delete", "Person not found");
            }
        }

        public void Update(Guid id, string firstName, string lastName, string email)
        {
            var person = GetPerson(id);
            if (person == null)
            {
                _notificationContainer.Add("Update", "Person not found");
                return;
            }

            person.ChangeName(firstName, lastName);
            person.ChangeEmail(email);

            if(!person.IsValid)
            {
                _notificationContainer.AddRange(person.Errors);
                return;
            }

            _repository.Update(person);
        }
    }
}
