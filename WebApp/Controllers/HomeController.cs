using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;
using WebApp.Models.Home;
using Domain;
using System;
using WebApp.Models;
using System.Diagnostics;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var homePage = new HomePageViewModel();
            var clientPerson = new RestClient();
            var requestPerson = new RestRequest("http://localhost:50139/api/people/", DataFormat.Json);
            var responsePerson = clientPerson.Get<List<Person>>(requestPerson);
            var personCount = responsePerson.Data.Count;

            var clientCountry = new RestClient();
            var requestCountry = new RestRequest("http://localhost:63454/api/countries/", DataFormat.Json);
            var responseCountry = clientCountry.Get<List<Country>>(requestCountry);
            var countryCount = responseCountry.Data.Count;

            var clientState = new RestClient();
            var requestState = new RestRequest("http://localhost:63454/api/states/", DataFormat.Json);
            var responseState = clientState.Get<List<State>>(requestState);
            var stateCount = responseState.Data.Count;

            homePage.PersonCount = personCount;
            homePage.CountryCount = countryCount;
            homePage.StateCount = stateCount;

            return View(homePage);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
