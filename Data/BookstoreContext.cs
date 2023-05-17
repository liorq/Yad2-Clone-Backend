using Microsoft.EntityFrameworkCore;
using asp.net_workshop_real_app_public.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace asp.net_workshop_real_app_public.Data
{
    /*public class BookstoreContext : DbContext*/
    public class BookstoreContext : IdentityDbContext<AppUser>
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options):base(options)
        {
        }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }

    }
}
