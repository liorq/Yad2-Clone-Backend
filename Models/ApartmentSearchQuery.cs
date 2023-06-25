using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace asp.net_workshop_real_app_public.Models
{
    public class ApartmentSearchQuery : Apartment
    {
      
        public Guid searchId { get; set; }
        public string? freeSearchText { get; set; } = "";

        public double? minFloor { get; set; } = 0;
        public double? minSqm { get; set; } = 0;
        public double? minPrice { get; set; } = 0;
        public double? maxPrice { get; set; } = 0;
        public double? minRooms { get; set; } = 0;
        public double? maxRooms { get; set; } = 0;
        public double? maxFloor { get; set; } = 0;
        public double? maxSqm { get; set; } = 0;
        //public string[]? arrayOfTypeProperty { get; set; } =null;
        public string[] arrayOfTypeProperty { get; set; } = new string[]
                {
            "כל סוגי הנכסים",
            "דירות",
            "דירה",
            "דירת גן",
            "גג/פנטהאוז",
            "דופלקס",
            "תיירות ונופש",
            "מרתף/פרטר",
            "טריפלקס",
            "יחידת דיור",
            "סטודיו/לופט",
            "בתים",
            "בית פרטי/קוטג'",
            "דו משפחתי",
            "משק חקלאי/נחלה",
            "משק עזר",
            "סוגים נוספים",
            "מגרשים",
            "דיור מוגן",
            "בניין מגורים",
            "מחסן",
            "חניה",
            "קב' רכישה/ זכות לנכס",
            "כללי"
                };


        public AppUser? person { get; set; }
  

    }
}
