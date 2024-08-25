using ETickets.Models;
using ETickets.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;



namespace ETickets.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository cart;
        private readonly UserManager<ApplicationUser> userManager;
        public CartController(ICartRepository cart, UserManager<ApplicationUser> userManager)
        {
            this.cart = cart;
            this.userManager = userManager;
        }
        public IActionResult Index(int id)
        {
            var userId = userManager.GetUserId(User);
            if (id != 0)
            {
                ShoppingCart shoppingCart = new()
                {
                    MovieId = id,
                    Count = 1,
                    ApplicationUserId = userId
                };
                cart.Create(shoppingCart);
                cart.Commit();
                return RedirectToAction("Index", "Movie");
            }
            var result = cart.Get(x => x.ApplicationUserId == userId, x => x.Movie);
            TempData["result"] = JsonConvert.SerializeObject(result);
            ViewBag.Total = result.Sum(x => x.Count * x.Movie.Price);
            return View(result);
        }
        public IActionResult Increament(int cartId)
        {
            var result = cart.Get(x => x.Id == cartId).FirstOrDefault();
            result.Count++;
            cart.Commit();
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Decreament(int cartId)
        {
            var result = cart.Get(x => x.Id == cartId).FirstOrDefault();
            if (result.Count == 1)
            {
                cart.Delete(result);
            }
            else
            {
                result.Count--;
            }
            cart.Commit();
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Delete(int cartId)
        {
            var result = cart.Get(x => x.Id == cartId).FirstOrDefault();
            cart.Delete(result);
            cart.Commit();
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Payment(ShoppingCart shoppingCart)
        {
            var obj1 = (string)TempData["result"];
            var items = JsonConvert.DeserializeObject<IEnumerable<ShoppingCart>>(obj1);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Checkout/Success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Checkout/Cancel",
            };

            foreach (var model in items)
            {
                var result = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = model.Movie.Name,
                            Description = model.Movie.Description,
                        },
                        UnitAmount = (long)model.Movie.Price * 100,
                    },
                    Quantity = model.Count,
                }; options.LineItems.Add(result);
            }

            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }
    }
}
