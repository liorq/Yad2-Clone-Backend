using asp.net_workshop_real_app_public.Data;
using asp.net_workshop_real_app_public.Repositories;
using Microsoft.EntityFrameworkCore;

namespace asp.net_workshop_real_app_public
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BookstoreContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("WorkshopRealAPIPublic")));
            builder.Services.AddControllers().AddNewtonsoftJson(opt => 
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddTransient<IBooksRepository, BooksRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}