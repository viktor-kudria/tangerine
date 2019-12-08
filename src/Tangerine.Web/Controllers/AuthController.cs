using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangerine.Core.Models.Auth;
using Tangerine.Data;
using Tangerine.Web.Models.Auth;

namespace Tangerine.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AuthController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Login(string returnUrl = null)
        {
            if (_appDbContext.Users.FirstOrDefaultAsync(x => true).Result == null)
            {
                _appDbContext.Users.Add(new User()
                {
                    Login = "admin",
                    Password = "admin"
                });

                _appDbContext.SaveChanges();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(
                    u => u.Login == viewModel.Login &&
                            u.Password == viewModel.Password);

                if (user != null)
                {
                    await Authenticate(user.Login);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Password or login is incorrect");
            }

            return View(viewModel);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }
    }
}
