using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace asp.net_workshop_real_app_public.Models
{
    public class AppUser : IdentityUser
    {
        //[Required(ErrorMessage = "Please add a title")]
        //    public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
