using ETickets._Repository.IRepository;
using ETickets._Repository;
using ETickets.Data;
using ETickets.Models;
using ETickets.Repositor.IRepository;
using ETickets.Repositor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using ETickets.Repository.IRepository;
using ETickets.Repository;
using Stripe;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
        //Identity Service
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
        AddEntityFrameworkStores<ApplicationDbContext>();



        builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IActorRepository, ActorRepository>();
        builder.Services.AddScoped<IActorMovieRepository, ActorMovieRepository>();
        builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
        builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();


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
