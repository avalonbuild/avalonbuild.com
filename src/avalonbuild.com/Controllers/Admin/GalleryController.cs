using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace avalonbuild.com.Controllers
{
    [Authorize]
    public class GalleryAdminController : Controller
    {
        private IHostingEnvironment _env;

        public GalleryAdminController(IHostingEnvironment env)
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
