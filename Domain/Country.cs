using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlPhoto { get; set; }
        public virtual IList<State> States { get; set; }
    }

    public class CountryResponse
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String UrlPhoto { get; set; }
        public virtual IList<State> States { get; set; }
    }

}
