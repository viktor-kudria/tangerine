using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
