using ETickets._Repository.IRepository;
using ETickets._Repository;
using ETickets.Data;
using ETickets.Models;
using ETickets.Repositor.IRepository;
using ETickets.Repositor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Configure Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Configure DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add repositories
        builder.Services.AddScoped<IActorRepository, ActorRepository>();
        builder.Services.AddScoped<IActorMovieRepository, ActorMovieRepository>();
        builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
        builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Movies}/{action=Index}/{id?}");

        app.Run();
    }
}
