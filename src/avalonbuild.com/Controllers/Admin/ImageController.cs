using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace avalonbuild.com.Controllers
{
    [Authorize]
    public class ImageAdminController : Controller
    {
        private IHostingEnvironment _env;

        public ImageAdminController(IHostingEnvironment env)
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
