using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonRegistration.Application;
using PersonRegistration.Application.ViewModels;
using ProjectRegistration.FrontEnd.Models;
using static Newtonsoft.Json.JsonConvert;

namespace ProjectRegistration.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private const string ApiUrl = "http://localhost:47081/";   
        public async Task<IActionResult> Index(SearchPersonViewModel model)
        {

            model.Persons = await GetRequestWebApi<List<PersonViewModel>>($"person/search/{model.Text}");

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonViewModel person)
        {

            var res = await PostRequestWebApi("/Person/Insert", person);

            if (res.IsSuccessStatusCode)
            {
               return RedirectToAction("Index");
            }

            if (res.StatusCode == HttpStatusCode.BadRequest)
            {
               var errors = DeserializeObject<List<dynamic>>(res.Content.ReadAsStringAsync().Result);
               errors.ForEach(x => ModelState.AddModelError((string)x.key, (string)x.value));
            }

            return View(person);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id = "")
        {

            var person = await GetRequestWebApi<PersonViewModel>($"/Person/Delete/{id}");

            if (person == null)
            {
                 return NotFound();
            }

            return View(person);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id = "")
        {

            var person = await GetRequestWebApi<PersonViewModel>($"/Person/Get/{id}");

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(string id)
        {

            await DeleteRequestWebApi<PersonViewModel>($"/Person/Delete/{id}");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(PersonViewModel person)
        {

            var res = await PostRequestWebApi("/Person/Update", person);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            if (res.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = DeserializeObject<List<dynamic>>(res.Content.ReadAsStringAsync().Result);
                errors.ForEach(x => ModelState.AddModelError((string)x.key, (string)x.value));
            }

            return View(person);
        }

        private static void ConfigureRequest(HttpClient client)
        {
            client.BaseAddress = new Uri(ApiUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<HttpResponseMessage> PostRequestWebApi(string url, object data)
        {

            using (var client = new HttpClient())
            {

                ConfigureRequest(client);

                var jsonString = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                return await client.PostAsync(url, content);

            }
        }

        private async Task<T> GetRequestWebApi<T>(string url, bool deleteMethod = false)
        {

            using (var client = new HttpClient())
            {
                IList<PersonViewModel> persons = new List<PersonViewModel>();

                ConfigureRequest(client);

                HttpResponseMessage res = await client.GetAsync(url);

                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;

                    return DeserializeObject<T>(response);
                }

            }

            return default(T);
        }

        private async Task<HttpResponseMessage> DeleteRequestWebApi<T>(string url, bool deleteMethod = false)
        {

            using (var client = new HttpClient())
            {
                IList<PersonViewModel> persons = new List<PersonViewModel>();

                ConfigureRequest(client);

                HttpResponseMessage res = await client.DeleteAsync(url);

                return res;

            }

    
        }
    }
}
