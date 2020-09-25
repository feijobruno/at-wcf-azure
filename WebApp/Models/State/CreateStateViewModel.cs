using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Country;

namespace WebApp.Models.State
{
    public class CreateStateViewModel
    {
        public IFormFile Photo { get; set; }
        public string UrlPhoto { get; set; }
        public string Name { get; set; }
        public virtual CountryViewModel Country { get; set; }
    }
}
