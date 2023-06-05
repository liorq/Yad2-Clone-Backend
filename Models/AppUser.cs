using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace asp.net_workshop_real_app_public.Models
{
    public class AppUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        
        public DateTime? BirthDate { get; set; }

        public string HouseNumber { get; set; }
        public ICollection<Apartment> Apartments{ get; set; }

    }
}
