using Microsoft.AspNetCore.Mvc;

namespace Tangerine.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
