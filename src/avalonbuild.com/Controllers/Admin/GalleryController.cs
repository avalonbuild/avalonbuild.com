using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using avalonbuild.com.Data;
using avalonbuild.com.Models;

namespace avalonbuild.com.Controllers
{
    [Authorize]
    public class GalleryAdminController : Controller
    {
        private readonly ImageDbContext _images;
        private IHostingEnvironment _env;
        private readonly ILogger _logger;

        public GalleryAdminController(ImageDbContext images,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            _images = images;
            _env = env;
            _logger = loggerFactory.CreateLogger<ImageController>();
        }

        [Route("/admin/galleries")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
