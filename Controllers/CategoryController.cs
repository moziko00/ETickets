using ETickets._Repository;
using ETickets._Repository.IRepository;
using ETickets.Data;
using ETickets.Models;
using ETickets.Repositor.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMoviesRepository movieRepository;

        public CategoryController(ICategoryRepository categoryRepository,IMoviesRepository movieRepository)
        {
            this.categoryRepository = categoryRepository;
            this.movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            var category = categoryRepository.GetAll();
            return View(category);
        }

        public IActionResult Details(int id)
        {
            var movies = movieRepository.Get(x => x.CategoryId == id, x => x.Category
            , x => x.Cinema);

            return View(movies);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Create(category);
                categoryRepository.Commit();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Edit(int id)
        {
            var result = categoryRepository.Get(x => x.Id == id).FirstOrDefault();
            return result != null ? View(result) : RedirectToAction("NotFound", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            categoryRepository.Edit(category);
            categoryRepository.Commit();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var result = categoryRepository.Get(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                categoryRepository.Delete(result);
                categoryRepository.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }
    }
}
