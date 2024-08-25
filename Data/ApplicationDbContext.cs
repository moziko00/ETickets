using ETickets.Models;
using ETickets.Models.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ETickets.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ActorMovie>()
                .HasKey(am => new { am.ActorsId, am.MoviesId });

            builder.Entity<ActorMovie>()
                .HasOne<Movie>(am => am.Movie)
                .WithMany(m => m.ActorMovies)
                .HasForeignKey(am => am.MoviesId);

            builder.Entity<ActorMovie>()
                .HasOne<Actor>(am=>am.Actor)
                .WithMany(a=>a.ActorMovies)
                .HasForeignKey(am=>am.ActorsId);




            base.OnModelCreating(builder);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }


        //public DbSet<ApplicationUserVM> ApplicationUserVM { get; set; } = default!;
        //public DbSet<ETickets.Models.ViewModel.LoginVM> LoginVM { get; set; } = default!;
        //public DbSet<ETickets.Models.ViewModel.RoleVM> RoleVM { get; set; } = default!;
    }
}
