using ETickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ETickets.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("DefaultConnetion");
            optionsBuilder.UseSqlServer(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>()
            .HasKey(am => new { am.ActorsId, am.MoviesId });

            modelBuilder.Entity<ActorMovie>()
                .HasOne<Movie>(am => am.Movie)
                .WithMany(m => m.ActorMovies)
                .HasForeignKey(am => am.MoviesId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne<Actor>(am => am.Actor)
                .WithMany(a => a.ActorMovies)
                .HasForeignKey(am => am.ActorsId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
