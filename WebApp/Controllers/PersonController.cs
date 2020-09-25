using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RestSharp;
using System.Collections.Generic;
using Domain;
using System;
using WebApp.Models.Person;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace WebApp.Controllers
{
    public class PersonController : Controller
    {
        // GET: PersonController
        public ActionResult Index()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:50139/api/people/", DataFormat.Json);
            var response = client.Get<List<Person>>(request);
            return View(response.Data);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:50139/api/people/" + id, DataFormat.Json);
            var response = client.Get<Person>(request);
            return View(response.Data);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            var clientCountry = new RestClient();
            var requestCountry = new RestRequest("http://localhost:63454/api/countries/", DataFormat.Json);
            var responseCountry = clientCountry.Get<List<Country>>(requestCountry);

            var clientState = new RestClient();
            var requestState = new RestRequest("http://localhost:63454/api/states/", DataFormat.Json);
            var responseState = clientState.Get<List<State>>(requestState);

            ViewBag.Countries = responseCountry.Data;
            ViewBag.States = responseState.Data;
            return View();

        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                var urlPhoto = UploadPhoto(person.Photo);
                person.UrlPhoto = urlPhoto;

                var client = new RestClient();
                var request = new RestRequest("http://localhost:50139/api/people/", DataFormat.Json);
                request.AddJsonBody(person);
                var response = client.Post<CreatePersonViewModel>(request);
                return Redirect("/person/index");
            }
            return BadRequest();
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:50139/api/people/" + id, DataFormat.Json);
            var response = client.Get<CreatePersonViewModel>(request);

            var clientCountry = new RestClient();
            var requestCountry = new RestRequest("http://localhost:63454/api/countries/", DataFormat.Json);
            var responseCountry = clientCountry.Get<List<Country>>(requestCountry);

            var clientState = new RestClient();
            var requestState = new RestRequest("http://localhost:63454/api/states/", DataFormat.Json);
            var responseState = clientState.Get<List<State>>(requestState);

            ViewBag.Countries = responseCountry.Data;
            ViewBag.States = responseState.Data;
            return View(response.Data);

 
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreatePersonViewModel person)
        {
            try
            {
 
                var client = new RestClient();
                var request = new RestRequest("http://localhost:50139/api/people/" + id, DataFormat.Json);
                request.AddJsonBody(person);

                var response = client.Put<CreatePersonViewModel>(request);
                return Redirect("/person/index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:50139/api/people/" + id, DataFormat.Json);
            var response = client.Get<Person>(request);

            return View(response.Data);
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Person person)
        {
            try
            {
                var client = new RestClient();

                var request = new RestRequest("http://localhost:50139/api/people/" + id, DataFormat.Json);
                var response = client.Delete<Country>(request);

                return Redirect("/person");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Friends(int id)
        {
            var viewModel = new FriendsListViewModel();

            var clientFriend = new RestClient();
            var requestFriend = new RestRequest("http://localhost:50139/api/people/{id}/friends" + id, DataFormat.Json);
            var responseFriend = clientFriend.Get<List<PersonViewModel>>(requestFriend);

            var clientPeople = new RestClient();
            var requestPeople = new RestRequest("http://localhost:50139/api/people/", DataFormat.Json);
            var responsePeople = clientPeople.Get<List<PersonViewModel>>(requestPeople);

            viewModel.Friends = responseFriend.Data;
            viewModel.People = responsePeople.Data;
            viewModel.Person = viewModel.People.First(x => x.Id == id);
            viewModel.People.Remove(viewModel.Person);

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Friends(int id, CreateFriendViewModel viewModel)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:50139/api/people/{id}/friends", DataFormat.Json);
            request.AddJsonBody(viewModel);
            client.Post<CreatePersonViewModel>(request);

            return Redirect("/person/details/"+ id);
        }

        private string UploadPhoto(IFormFile photo)
        {
            var reader = photo.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=brunofeijo;AccountKey=kryyZJNitE/n0PTEiTsKJBW42w8aGSBRQ4K9JqCdSC4oHKh2Znsc/ePLgzrzWHGpL9Q5dRO2v9BtRkAsh9/SiQ==;EndpointSuffix=core.windows.net");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("person-img");
            container.CreateIfNotExists();
            //var blob = container.GetBlockBlobReference(fileName);
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString().Replace("-", "") + ".jpg");
            blob.UploadFromStream(reader);
            var cloudUrl = blob.Uri.ToString();
            return cloudUrl;
        }
    }
}
