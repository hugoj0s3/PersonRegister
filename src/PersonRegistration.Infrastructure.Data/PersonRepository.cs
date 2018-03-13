using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PersonRegistration.Domain.Persons;
using PersonRegistration.Domain.Persons.Repository;

namespace PersonRegistration.Infrastructure.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DbContext _dbContext;

        public PersonRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(Person person)
        {
            _dbContext.Add(person);
        }

        public Person GetPerson(Guid personId)
        {
            return _dbContext.Set<Person>().FirstOrDefault(x => x.Id == personId);
        }

        public IList<Person> SearchPersons(string text)
        {
            return _dbContext.Set<Person>()
                .Where(x => x.Email.Contains(text) ||
                            x.FirstName.Contains(text) ||
                            x.LastName.Contains(text))
                .ToList();
        }

        public bool Delete(Guid personId)
        {
            var person = GetPerson(personId);
            if (person == null)
            {
                return false;
            }

            _dbContext.Remove(person);
            return true;
        }

        public void Update(Person person)
        {
            _dbContext.Update(person);
        }
    }
}
