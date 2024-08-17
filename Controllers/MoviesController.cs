using ETickets._Repository;
using ETickets._Repository.IRepository;
using ETickets.Data;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ETickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }
        public IActionResult Index()
        {
            var movies = moviesRepository.GetAll(m => m.Category, m => m.Cinema);
            return View(movies);
        }


        //public IActionResult Edit(int id)
        //{
        //    var movie = moviesRepository.Get(x => x.Id == id).FirstOrDefault(); // Ensure you are getting a single entity
        //    return movie != null ? View(movie) : RedirectToAction("NotFound", "Home");
        //}

        public IActionResult Edit(int id)
        {
            var movie = moviesRepository.Get(x => x.Id == id).FirstOrDefault();


            return movie != null ? View(movie) : RedirectToAction("NotFound", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie); // Return to the view if the model state is invalid
            }

            moviesRepository.Edit(movie);
            moviesRepository.Commit();
            return RedirectToAction("Index");
        }
        public IActionResult Search(string searchString)
        {
            var movie= moviesRepository.Get(x => x.Name == searchString, x => x.Category, x => x.Cinema).ToList();
            return View ("Index", movie);
        }


        //public IActionResult Details(int id)
        //{
        //    var movie = moviesRepository.Get(x => x.Id == id).FirstOrDefault();

        //    return movie != null ? View(movie) : RedirectToAction("NotFound", "Home");
        //}

        public IActionResult Delete(int id)
        {
            var movie = moviesRepository.Get(x => x.Id == id).FirstOrDefault();
            if (movie != null)
            {
                moviesRepository.Delete(movie);
                moviesRepository.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

    }
}
