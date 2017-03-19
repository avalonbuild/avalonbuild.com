using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace avalonbuild.com.Controllers
{
    public class AdminController : Controller
    {
        [Route("/admin")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

    }
}
