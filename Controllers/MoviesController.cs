using ETickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ETickets.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var movies = context.Movies
                .Include(m => m.Cinema)    
                .Include(m => m.Category)  
                .ToList();  

            return View(movies);
        }
        public IActionResult Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                
                return RedirectToAction(nameof(Index));
            }

            searchString = searchString.ToLower();

            var movies = context.Movies
                .Include(m => m.Cinema) 
                .Include(m => m.Category) 
                .Where(m => m.Name.ToLower().Contains(searchString))
                .ToList();

            if (movies.Count == 0)
            {
                
                return View("NotFound"); 
            }

            return View("Index", movies);
        }


        public IActionResult Details(int id)
        {
            var movie = context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Category)
                .Include(m => m.ActorMovies) 
                    .ThenInclude(am => am.Actor) 
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

    }
}
