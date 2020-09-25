using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Country;
using WebApp.Models.State;

namespace WebApp.Models.Person
{
    public class CreatePersonViewModel
    {
        public IFormFile Photo { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlPhoto { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public List<CountryViewModel> Countries { get; set; }
        public int CountryId { get; set; }
        public List<StateViewModel> States { get; set; }
        public int StateId { get; set; }
    }
}
