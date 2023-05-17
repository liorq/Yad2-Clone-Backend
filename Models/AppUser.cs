﻿using Microsoft.AspNetCore.Identity;

namespace asp.net_workshop_real_app_public.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
