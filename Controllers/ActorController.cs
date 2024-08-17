using ETickets.Data;
using ETickets.Repositor.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorRepository actorRepository;

        public ActorController(IActorRepository actorRepository)
        {
           this.actorRepository = actorRepository;
        }

        public IActionResult Index(int id)
        {
            //var actor = context.Actors
            //    .Include(a => a.ActorMovies)
            //        .ThenInclude(am => am.Movie) // Include movies
            //    .FirstOrDefault(a => a.Id == id);
            var actor = actorRepository.Get(a => a.Id == id, a => a.ActorMovies, a => a.ActorMovies.Select(mo => mo.Movie));
            return View(actor);
        }
    }
}
