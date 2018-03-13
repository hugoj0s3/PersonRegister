using System;
using System.Collections.Generic;
using System.Linq;
using PersonRegistration.Application.ViewModels;
using PersonRegistration.Domain.Core;
using PersonRegistration.Domain.Persons;
using PersonRegistration.Domain.Persons.Services;

namespace PersonRegistration.Application
{
    public class PersonAppService : IDisposable, IPersonAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonService _service;
        private readonly NotificationContainer _notificationContainer;

        public PersonAppService(IUnitOfWork unitOfWork, 
            IPersonService service, 
            NotificationContainer notificationContainer)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _notificationContainer = notificationContainer;
        }

        public PersonViewModel GetPerson(Guid personId)
        {
            var person = _service.GetPerson(personId);

            return new PersonViewModel()
            {
                Id = personId,
                Email = person.Email,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }

        public IList<PersonViewModel> SearchPersons(string text)
        {
            return _service.SearchPersons(text)
                .Select(x =>
                    new PersonViewModel()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    }
                ).ToList();
        }

        public Guid Insert(PersonViewModel person)
        {
            try
            {
                var id = _service.Insert(person.FirstName, person.LastName, person.Email);
                if (_notificationContainer.HasErrors)
                {
                    return id;
                }

                _unitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rolback();
                throw ex;
            }
           
        }

        public void Delete(Guid personId)
        {
            try
            {
                _service.Delete(personId);
                if (_notificationContainer.HasErrors)
                {
                    return;
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rolback();
                throw ex;
            }


        }

        public void Update(PersonViewModel person)
        {
            try
            {
               
                _service.Update(person.Id ?? Guid.Empty, person.FirstName, person.LastName, person.Email);
                if (_notificationContainer.HasErrors)
                {
                    return;
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rolback();
                throw ex;
            }

        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
