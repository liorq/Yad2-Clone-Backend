namespace asp.net_workshop_real_app_public.Models
{
    public class Pic
    {

        public Guid picId { get; set; }
        public Apartment apartment { get; set; }
        public string value { get; set; }
    }
}
