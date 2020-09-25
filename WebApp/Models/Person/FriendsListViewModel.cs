using System.Collections.Generic;

namespace WebApp.Models.Person
{
    public class FriendsListViewModel
    {
        public List<PersonViewModel> People { get; set; }
        public PersonViewModel Person { get; set; }
        public List<PersonViewModel> Friends { get; set; }
    }
}
