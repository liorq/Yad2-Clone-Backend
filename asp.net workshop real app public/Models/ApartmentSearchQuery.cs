using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace asp.net_workshop_real_app_public.Models
{
    public class ApartmentSearchQuery : Apartment
    {
      
        public Guid searchId { get; set; }
        public string? freeSearchText { get; set; }

        public double? minFloor { get; set; } = 0;
        public double? minSqm { get; set; } = 0;
        public double? minPrice { get; set; } = 0;
        public double? maxPrice { get; set; } = 0;
        public double? minRooms { get; set; } = 0;
        public double? maxRooms { get; set; } = 0;
        public double? maxFloor { get; set; } = 0;
        public double? maxSqm { get; set; } = 0;
        //public string[]? arrayOfTypeProperty { get; set; }
        public AppUser? person { get; set; }
  

    }
}
