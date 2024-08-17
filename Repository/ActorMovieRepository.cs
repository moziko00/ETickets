using ETickets._Repository;
using ETickets.Data;
using ETickets.Models;
using ETickets.Repositor.IRepository;

namespace ETickets.Repositor
{
    public class ActorMovieRepository : Repository<ActorMovie>, IActorMovieRepository
    {
        public ActorMovieRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
