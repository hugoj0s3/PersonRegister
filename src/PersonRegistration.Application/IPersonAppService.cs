using System;
using System.Collections.Generic;
using PersonRegistration.Application.ViewModels;
using PersonRegistration.Domain.Persons;

namespace PersonRegistration.Application
{
    public interface IPersonAppService
    {
        void Delete(Guid personId);
        void Dispose();
        PersonViewModel GetPerson(Guid personId);
        Guid Insert(PersonViewModel person);
        IList<PersonViewModel> SearchPersons(string text);
        void Update(PersonViewModel person);
    }
}