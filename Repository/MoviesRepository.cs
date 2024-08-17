using ETickets._Repository.IRepository;
using ETickets.Data;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ETickets._Repository
{
    public class MoviesRepository : Repository<Movie>, IMoviesRepository
    {
        private readonly ApplicationDbContext context1;

        public MoviesRepository(ApplicationDbContext context) : base(context)
        {
            this.context1 = context;
        }
        public async Task<IEnumerable<Movie>> SearchByNameAsync(string name)
        {
            return await context1.Movies
                .Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }
    }
}
