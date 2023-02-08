﻿using Microsoft.EntityFrameworkCore;
using asp.net_workshop_real_app_public.Models;
namespace asp.net_workshop_real_app_public.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options):base(options)
        {
        }
        public DbSet<BookModel> Books { get; set; }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=WorkShopRealAPIPublic;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}
