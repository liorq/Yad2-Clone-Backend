using Microsoft.EntityFrameworkCore;
using asp.net_workshop_real_app_public.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace asp.net_workshop_real_app_public.Data
{
    public class ApartementContext : IdentityDbContext<AppUser>
    {

        public ApartementContext(DbContextOptions<ApartementContext> options) : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }

    }
}
