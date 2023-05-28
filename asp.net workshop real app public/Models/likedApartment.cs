namespace asp.net_workshop_real_app_public.Models
{
    public class likedApartment
    { 
        public int Id { get; set; }
        public Guid apartmentId { get; set; }
        public string? email { get; set; }

    }
}
