using System;
using System.Collections.Generic;

namespace PersonRegistration.Domain.Persons.Services
{
    public interface IPersonService
    {
        Person GetPerson(Guid personId);
        IList<Person> SearchPersons(string text);
        Guid Insert(string firstName, string lastName, string email);
        void Delete(Guid personId);
        void Update(Guid id, string firstName, string lastName, string email);
    }
}
