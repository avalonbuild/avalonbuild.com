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
    public class ImageController : Controller
    {
        private readonly ImageDbContext _images;
        private readonly FileDbContext _files;
        private IHostingEnvironment _env;
        private readonly ILogger _logger;

        public ImageController(ImageDbContext images,
            FileDbContext files,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            _images = images;
            _files = files;
            _env = env;
            _logger = loggerFactory.CreateLogger<ImageController>();
        }

        [Route("/admin/images")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin/images/get")]
        public async Task<IActionResult> Get()
        {
            var images = await _images.Images.ToListAsync();

            return Json(images);
        }

        [Route("/admin/images/add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Image model)
        {
            if (!ModelState.IsValid || Request.Form.Files.Count != 1)
            {
                return BadRequest("Image name and file are required.");
            }

            var file = new Models.File
            {
                Name = "images/" + model.Name,
                MimeType = "image/png"
            };

            using (var memoryStream = new MemoryStream())
            {
                await Request.Form.Files[0].CopyToAsync(memoryStream);
                file.Data = memoryStream.ToArray();
            }

            var fileresult = _files.Files.Add(file);
            await _files.SaveChangesAsync();

            var image = new Models.Image
            {
                Name = model.Name,
                Title = model.Title,
                Description = model.Description,
                FileName = file.Name
            };

            var imageresult = _images.Images.Add(image);
            await _images.SaveChangesAsync();

            string message = $"Image uploaded successfully.";

            return Json(message);
        }

        [Route("/admin/images/delete/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _images.Images.SingleOrDefaultAsync(i => i.ID == id);

            if (image != null)
            {
                var file = new Models.File {
                    Name = image.FileName
                };

                _files.Attach(file);
                _files.Remove(file);

                await _files.SaveChangesAsync();

                _images.Remove(image);
                await _images.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
