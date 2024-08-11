using ETickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class ActorController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index(int id)
        {
            var actor = context.Actors
                .Include(a => a.ActorMovies)
                    .ThenInclude(am => am.Movie) // Include movies
                .FirstOrDefault(a => a.Id == id);
            return View(actor);
        }
    }
}
