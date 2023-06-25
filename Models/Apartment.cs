using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp.net_workshop_real_app_public.Models
{
    public class Apartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid apartmentId { get; set; }
        public string? name { get; set; }
        public string? category { get; set; }
        public string? street { get; set; }
        public double? price { get; set; }
        public bool hasAirConditioning { get; set; }
        public bool hasWindowBars { get; set; }
        public bool hasElevator { get; set; }
        public bool hasKosherKitchen { get; set; }
        public bool hasSolarHeater { get; set; }
        public bool hasAccessibilityForDisabled { get; set; }
        public bool isRenovated { get; set; }
        public bool isSmartHome { get; set; }
        public bool hasStorage { get; set; }
        public bool hasCentralAirConditioning { get; set; }
        public bool hasFurniture { get; set; }
        public bool isResidentialUnit { get; set; }
        public bool agreedToGetUpdates { get; set; }
        public double? totalSquareFootage { get; set; }
        public string? city { get; set; }
        public string? conditionOfProperty { get; set; }
        public string? des { get; set; } = "";
        public string? email { get; set; }
        public double? floor { get; set; }
        public double? houseNumber { get; set; }
        public bool  immediate { get; set; }
        public string? parking { get; set; }
        public string? personName { get; set; }
        public string? porch { get; set; }
        public double? roomNumber { get; set; }
        public double? totalFloorInBuilding { get; set; }
        public string?   typeOfProperty { get; set; }
        public string? dateOfEntering { get; set; }
        public string? comment { get; set; }
        public AppUser? person { get; set; }
        [NotMapped]
        public List<string>? pics { get; set; }
    }
}
