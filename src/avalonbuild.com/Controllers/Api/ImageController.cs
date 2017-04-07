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

namespace avalonbuild.com.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    public class ImageController : Controller
    {
        private readonly ImageDbContext _images;
        private readonly FileDbContext _files;
        private readonly ILogger _logger;

        public ImageController(ImageDbContext images,
            FileDbContext files,
            ILoggerFactory loggerFactory)
        {
            _images = images;
            _files = files;
            _logger = loggerFactory.CreateLogger<ImageController>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _images.Images.ToListAsync();

            return Json(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Image model)
        {
            if (!ModelState.IsValid || Request.Form.Files.Count != 1)
            {
                return BadRequest("Image file is required.");
            }

            if (model.Name == null || model.Name == "")
            {
                model.Name = Request.Form.Files[0].FileName;
            }

            var file = new Models.File
            {
                Name = "images/" + model.Name,
                MimeType = Request.Form.Files[0].ContentType
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

        [HttpDelete("{id}")]
        [Authorize]
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
