using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlPhoto { get; set; }
        [JsonIgnore]
        public virtual Country Country { get; set; }

    }

    public class StateResponse
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String UrlPhoto { get; set; }

        public virtual Country Country { get; set; }
    }

}