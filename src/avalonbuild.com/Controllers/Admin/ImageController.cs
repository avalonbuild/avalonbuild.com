using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace avalonbuild.com.Controllers
{
    [Authorize]
    public class ImageAdminController : Controller
    {
        private IWebHostEnvironment _env;

        public ImageAdminController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [Route("/admin/images")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
