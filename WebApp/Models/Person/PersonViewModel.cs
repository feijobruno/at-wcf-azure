using System;
using System.Collections.Generic;

namespace WebApp.Models.Person
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlPhoto { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }

    }
}
