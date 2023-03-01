namespace asp.net_workshop_real_app_public.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookModel>? Books { get; set; }
    }
}
