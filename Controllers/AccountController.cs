using ETickets.Models;
using ETickets.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserVM userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = userVM.Name,
                    Email = userVM.Email
                };
                await userManager.CreateAsync(user, userVM.Password);
                return RedirectToAction("Index", "Home");
            }
            return View(userVM);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginVM.Name);
                if (user != null)
                {
                    var result = await userManager.CheckPasswordAsync(user, loginVM.Password);
                    if (result)
                    {
                        await signInManager.SignInAsync(user, loginVM.RemmemberMe);
                        return RedirectToAction("Index", "Movies");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Password is Wrong, try again!");
                    }

                }
                else
                {
                    ModelState.AddModelError("Email", "Invalid Email");
                }

            }
            return View(loginVM);
        }
    }
}
