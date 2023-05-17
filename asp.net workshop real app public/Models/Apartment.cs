namespace asp.net_workshop_real_app_public.Models
{
    public class Apartment
    {
        public Guid apartmentId { get; set; }

        public string name { get; set; }
        public string category { get; set; }
        public string street { get; set; }
        public decimal price { get; set; }
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
    }

}
