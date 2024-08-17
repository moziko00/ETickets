using ETickets._Repository;
using ETickets._Repository.IRepository;
using ETickets.Data;
using ETickets.Models;

namespace ETickets.Repositor
{
    public class CinemaRepository : Repository<Cinema>, ICinemaRepository
    {
        public CinemaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
