using System;
using System.Collections.Generic;

namespace PersonRegistration.Domain.Persons.Repository
{
    public interface IPersonRepository
    {
        void Insert(Person person);

        Person GetPerson(Guid personId);

        IList<Person> SearchPersons(string text);

        bool Delete(Guid personId);

        void Update(Person person);

       
    }
}
