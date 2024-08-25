using ETickets.Models;
using ETickets.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            if (User.IsInRole("Admin"))
            {
                var result = roleManager.Roles.Select(x => new SelectListItem
                { Value = x.Name, Text = x.Name });
                ViewBag.roles = result;
            }
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
                    Email = userVM.Email,
                    PasswordHash=userVM.Password,
                    Address = userVM.Address
                };
                var result = await userManager.CreateAsync(user, userVM.Password);
                if(result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                        await userManager.AddToRoleAsync(user, userVM.Role);
                    else
                        await userManager.AddToRoleAsync(user, "User");

                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Movies");
                }
                ModelState.AddModelError("Password", "Don't Match Roles");
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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole user = new(roleVM.Name);
                await roleManager.CreateAsync(user);
                return RedirectToAction("CreateRole");
            }
            return View(roleVM);

        }
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index", "Movies");
        }
    }
}
