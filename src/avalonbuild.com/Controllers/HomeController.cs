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

        [Route("/referrals")]
        public IActionResult Referrals()
        {
            return View();
        }

        [Route("/work")]
        public IActionResult Work()
        {
            return View();
        }
    }
}
