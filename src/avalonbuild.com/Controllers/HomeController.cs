using Microsoft.AspNetCore.Mvc;

namespace avalonbuild.com.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/services")]
        public IActionResult Services()
        {
            return View();
        }

        [Route("/galleries")]
        public IActionResult Galleries()
        {
            return View();
        }

        [Route("/gallery/{name}")]
        public IActionResult Gallery(string name)
        {
            return View(name);
        }

        [Route("/referrals")]
        public IActionResult Referrals()
        {
            return View();
        }

    }
}
