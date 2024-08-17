using ETickets._Repository;
using ETickets._Repository.IRepository;
using ETickets.Data;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ICinemaRepository cinemaRepository;
        private readonly IMoviesRepository movieRepository;


        public CinemaController(ICinemaRepository cinemaRepository, IMoviesRepository movieRepository)
        {
            this.cinemaRepository = cinemaRepository;
            this.movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            var result = cinemaRepository.GetAll();
            return View(result);
        }

        public IActionResult Details(int id)
        {
            var movies = movieRepository.Get(x => x.CinemaId == id, x => x.Category, x => x.Cinema);

            return View(movies);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                cinemaRepository.Create(cinema);
                cinemaRepository.Commit();
                return RedirectToAction("Index");
            }
            return View(cinema);
        }


        public IActionResult Edit(int id)
        {
            var result = cinemaRepository.Get(x => x.Id == id).FirstOrDefault();
            return result != null ? View(result) : RedirectToAction("NotFound", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cinema cinema)
        {
            cinemaRepository.Edit(cinema);
            cinemaRepository.Commit();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var result = cinemaRepository.Get(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                cinemaRepository.Delete(result);
                cinemaRepository.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }
    }
}
