using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Country
{
    public class CreateCountryViewModel
    {
        public IFormFile Photo { get; set; }
        public string UrlPhoto { get; set; }
        public string Name { get; set; }

    }
}
