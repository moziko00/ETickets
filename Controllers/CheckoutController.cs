using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Cancel()
        {
            return View();
        }
    }
}
