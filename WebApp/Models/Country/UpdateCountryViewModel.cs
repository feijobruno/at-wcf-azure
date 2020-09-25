using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.State;

namespace WebApp.Models.Country
{
    public class UpdateCountryViewModel
    {
        public IFormFile Photo { get; set; }
        public string UrlPhoto { get; set; }
        public string Name { get; set; }
        public virtual IList<StateViewModel> States { get; set; }
    }
}
