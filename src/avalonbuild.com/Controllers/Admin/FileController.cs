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

namespace avalonbuild.com.Controllers
{
    public class FileController : Controller
    {
        private readonly FileDbContext _files;
        private IHostingEnvironment _env;
        private readonly ILogger _logger;

        public FileController(FileDbContext files,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            _files = files;
            _env = env;
            _logger = loggerFactory.CreateLogger<FileController>();
        }

        [Route("/file/{*name}")]
        public async Task<IActionResult> Img(string name)
        {
            var file = await _files.Files.SingleOrDefaultAsync(i => i.Name == name);

            if (file != null && file.Data != null)
                return File(file.Data, file.MimeType);

            return NotFound();
        }
    }
}
