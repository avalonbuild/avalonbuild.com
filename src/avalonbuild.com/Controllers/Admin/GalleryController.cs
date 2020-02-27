using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace avalonbuild.com.Controllers
{
    [Authorize]
    public class GalleryAdminController : Controller
    {
        private IWebHostEnvironment _env;

        public GalleryAdminController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [Route("/admin/galleries")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
