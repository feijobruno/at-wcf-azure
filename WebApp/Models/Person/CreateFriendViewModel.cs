using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Country;
using WebApp.Models.State;

namespace WebApp.Models.Person
{
    public class CreateFriendViewModel
    {
        public int PersonId { get; set; }
        public int[] FriendsIds { get; set; }
    }
}
