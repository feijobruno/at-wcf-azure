using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RestSharp;
using System.Collections.Generic;
using WebApp.Models.Country;
using Domain;
using System;

namespace WebApp.Controllers
{
    public class CountryController : Controller
    {
        // GET: CountryController
        public ActionResult Index()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:63454/api/countries/", DataFormat.Json);
            var response = client.Get<List<Country>>(request);
            return View(response.Data);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:63454/api/countries/" + id, DataFormat.Json);
            var response = client.Get<Country>(request);
            return View(response.Data);
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCountryViewModel country)
        {
            if (ModelState.IsValid)
            {
                var urlPhoto = UploadPhoto(country.Photo);
                country.UrlPhoto = urlPhoto;

                var client = new RestClient();
                var request = new RestRequest("http://localhost:63454/api/countries/", DataFormat.Json);
                request.AddJsonBody(country);
                var response = client.Post<CreateCountryViewModel>(request);
                return Redirect("/country/index");
            }
            return BadRequest();
        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:63454/api/countries/" + id, DataFormat.Json);
            var response = client.Get<Country>(request);

            return View(response.Data);
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Country country)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest("http://localhost:63454/api/countries/" + id, DataFormat.Json);
                request.AddJsonBody(country);
                var response = client.Put<Country>(request);

                return Redirect("/country/index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CountryController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:63454/api/countries/" + id, DataFormat.Json);
            var response = client.Get<Country>(request);

            return View(response.Data);
        }

        // POST: CountryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Country country)
        {
            try
            {
                var client = new RestClient();

                var request = new RestRequest("http://localhost:63454/api/countries/" + id, DataFormat.Json);
                var response = client.Delete<Country>(request);

                return Redirect("/country");
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
