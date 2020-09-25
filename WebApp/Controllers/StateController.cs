using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RestSharp;
using WebApp.Models.State;

namespace WebApp.Controllers
{
    public class StateController : Controller
    {
        // GET: StateController
        public ActionResult Index()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:63454/api/states/", DataFormat.Json);
            var response = client.Get<List<State>>(request);
            return View(response.Data);
        }

        // GET: StateController/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:63454/api/states/" + id, DataFormat.Json);
            var response = client.Get<State>(request);
            return View(response.Data);
        }

        // GET: StateController/Create
        public ActionResult Create()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:63454/api/countries/", DataFormat.Json);
            var response = client.Get<List<Country>>(request);
            
            ViewBag.Countries = response.Data;
            return View();
        }

        // POST: StateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStateViewModel state)
        {
            if (ModelState.IsValid)
            {
                var urlPhoto = UploadPhoto(state.Photo);
                state.UrlPhoto = urlPhoto;

                var client = new RestClient();
                var request = new RestRequest("http://localhost:63454/api/states/", DataFormat.Json);
                request.AddJsonBody(state);
                var response = client.Post<CreateStateViewModel>(request);

                return Redirect("/state/index");
            }
            return BadRequest();
        }

        // GET: StateController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:63454/api/states/" + id, DataFormat.Json);
            var response = client.Get<State>(request);

            var clientCountry = new RestClient();
            var requestCountry = new RestRequest("http://localhost:63454/api/countries/", DataFormat.Json);
            var responseCountry = clientCountry.Get<List<Country>>(requestCountry);

            ViewBag.Countries = responseCountry.Data;

            return View(response.Data);
        }

        // POST: StateController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateStateViewModel state)
        {
            try
            {
                if (state.Photo != null)
                {
                    var urlPhoto = UploadPhoto(state.Photo);
                    state.UrlPhoto = urlPhoto;
                }
                 
                var client = new RestClient();
                var request = new RestRequest("http://localhost:63454/api/states/" + id, DataFormat.Json);
                request.AddJsonBody(state);

                var response = client.Put<CreateStateViewModel>(request);
                return Redirect("/state/index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StateController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:63454/api/states/" + id, DataFormat.Json);
            var response = client.Get<State>(request);

            return View(response.Data);
        }

        // POST: StateController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest("http://localhost:63454/api/states/" + id, DataFormat.Json);
                var response = client.Delete<State>(request);

                return Redirect("/state");
            }
            catch
            {
                return View();
            }
        }
        private string UploadPhoto(IFormFile photo)
        {

            var reader = photo.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=brunofeijo;AccountKey=kryyZJNitE/n0PTEiTsKJBW42w8aGSBRQ4K9JqCdSC4oHKh2Znsc/ePLgzrzWHGpL9Q5dRO2v9BtRkAsh9/SiQ==;EndpointSuffix=core.windows.net");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("places-img");
            container.CreateIfNotExists();
            //var blob = container.GetBlockBlobReference(fileName);
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString().Replace("-", "") + ".jpg");
            blob.UploadFromStream(reader);
            var cloudUrl = blob.Uri.ToString();
            return cloudUrl;
        }
    }
}
