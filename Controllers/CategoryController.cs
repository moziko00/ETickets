using ETickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var category = context.Categories.ToList();
            return View(category);
        }

        public IActionResult Details(int id)
        {
            var movies = context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Category).Where(m => m.CategoryId == id).ToList();

            return View(movies);
        }
    }
}
