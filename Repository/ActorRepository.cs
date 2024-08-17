using ETickets._Repository;
using ETickets.Data;
using ETickets.Models;
using ETickets.Repositor.IRepository;

namespace ETickets.Repositor
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
