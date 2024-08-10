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
                // Redirect to Index or handle as needed
                return RedirectToAction(nameof(Index));
            }
            searchString = searchString.ToLower();

            var movies = context.Movies
                .Include(m => m.Cinema) // Eager load Cinema
                .Include(m => m.Category) // Eager load Category
                .Where(m => m.Name.ToLower().Contains(searchString))
                .ToList();
            return View("Index",movies);
        }

        public IActionResult Details(int id)
        {
            // Fetch the movie including related entities
            var movie = context.Movies
                .Include(m => m.Cinema) // Eager load Cinema
                .Include(m => m.Category) // Eager load Category
                .FirstOrDefault(m => m.Id == id); // Get the movie by ID

            // Check if the movie was found
            if (movie == null)
            {
                return NotFound(); // Return a 404 error if the movie was not found
            }

            // Pass the movie to the view
            return View(movie);
        }

    }
}
