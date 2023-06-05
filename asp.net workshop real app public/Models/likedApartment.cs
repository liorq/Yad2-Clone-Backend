using System.ComponentModel.DataAnnotations.Schema;

namespace asp.net_workshop_real_app_public.Models
{
    public class likedApartment
    { 
        public int Id { get; set; }
        public Guid likedApartmentId { get; set; }
        public AppUser? person { get; set; }
        public Apartment? apartment { get; set; }


    }
}
