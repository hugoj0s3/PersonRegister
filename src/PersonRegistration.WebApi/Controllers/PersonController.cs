using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonRegistration.Application;
using PersonRegistration.Application.ViewModels;
using PersonRegistration.Domain.Core;

namespace PersonRegistration.WebApi.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonAppService _appService;
        private readonly NotificationContainer _notificationContainer;

        public PersonController(IPersonAppService appService, NotificationContainer notificationContainer)
        {
            _appService = appService;
            _notificationContainer = notificationContainer;
        }

        [HttpGet]
        [Route("Person/Search/{text?}")]
        public ActionResult Search(string text = "")
        {
            return Ok(_appService.SearchPersons(text));
        }

        [HttpGet]
        [Route("Person/Get/{id}")]
        public ActionResult Get(string id)
        {
            return Ok(_appService.GetPerson(Guid.Parse(id))); ;
        }

        [HttpPost]
        [Route("Person/Insert")]
        public ActionResult Insert([FromBody]PersonViewModel person)
        {
            _appService.Insert(person);

            if (_notificationContainer.HasErrors)
            {
                return BadRequest(_notificationContainer.Notifications);
            }

            return Ok();
        }

        [HttpPost]
        [Route("Person/Update")]
        public ActionResult Update([FromBody]PersonViewModel person)
        {
            _appService.Update(person);

            if (_notificationContainer.HasErrors)
            {
                return BadRequest(_notificationContainer.Notifications);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("Person/Delete/{id}")]
        public ActionResult Delete(string id)
        {
            _appService.Delete(Guid.Parse(id));
            return Ok();
        }
    }
}