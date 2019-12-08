using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tangerine.Web.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return Content("Hop");
        }
    }
}