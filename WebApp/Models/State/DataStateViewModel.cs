using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Country;

namespace WebApp.Models.State
{
    public class DataStateViewModel
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string UrlPhoto { get; set; }
        public string Name { get; set; }
        public virtual CountryViewModel Country { get; set; }
    }
}
