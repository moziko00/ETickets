using ETickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class CinemaController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var cinemas = context.Cinemas.ToList();
            return View(cinemas);
        }
        public IActionResult Details(int id)
        {
            var movies = context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Category).Where(m=>m.CinemaId == id).ToList();

            return View(movies);
        }
    }
}
