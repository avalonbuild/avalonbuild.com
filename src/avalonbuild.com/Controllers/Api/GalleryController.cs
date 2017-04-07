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
using System.Threading.Tasks;
using avalonbuild.com.Data;
using avalonbuild.com.Models;

namespace avalonbuild.com.Controllers.Api
{
    [Route("api/[controller]")]
    public class GalleryController : Controller
    {
        private readonly ImageDbContext _images;
        private readonly ILogger _logger;

        public GalleryController(ImageDbContext images, ILoggerFactory loggerFactory)
        {
            _images = images;
            _logger = loggerFactory.CreateLogger<GalleryController>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var galleries = await _images.Galleries.Include(g => g.Images).ThenInclude(i => i.Image).ToListAsync();

            var model = new List<ViewModels.Gallery>();

            foreach (var gallery in galleries)
            {
                model.Add(GalleryModelToViewModel(gallery));
            }

            return Json(model);
        }

        [HttpGet("{id}", Name = "GetGallery")]
        public async Task<IActionResult> GetById(string id)
        {
            var gallery = await _images.Galleries.Include(g => g.Images).SingleOrDefaultAsync(i => i.ID.ToString() == id);

            if (gallery == null)
                return NotFound();

            return Json(GalleryModelToViewModel(gallery));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] ViewModels.Gallery gallery)
        {
            if (gallery == null)
                return BadRequest("Error creating gallery.");

            var dbGallery = new Models.Gallery{
                Name = gallery.Name,
                Title = gallery.Title,
                Description = gallery.Description
            };

            _images.Galleries.Add(dbGallery);

            foreach (var image in gallery.Images)
            {
                var dbImage = await _images.Images.SingleOrDefaultAsync(i => i.ID == image.ID);

                if (dbImage != null)
                    dbGallery.Images.Add(new GalleryImage { Image = dbImage});
            }

            await _images.SaveChangesAsync();

            string message = $"Gallery added successfully.";

            return Json(message);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var gallery = await _images.Galleries.Include(g => g.Images).FirstOrDefaultAsync(i => i.ID == id);

            if (gallery == null)
                return NotFound();

            _images.Galleries.Remove(gallery);

            await _images.SaveChangesAsync();

            return Ok();
        }

        private ViewModels.Gallery GalleryModelToViewModel(Models.Gallery gallery)
        {
                var modelGallery = new ViewModels.Gallery {
                    ID = gallery.ID,
                    Name = gallery.Name,
                    Title = gallery.Title,
                    Description = gallery.Description
                };

                foreach (var image in gallery.Images)
                {
                    var modelImage = new ViewModels.Image {
                        ID = image.Image.ID,
                        Name = image.Image.Name,
                        Title = image.Image.Title,
                        Description = image.Image.Description,
                        FileName = image.Image.FileName
                    };

                    modelGallery.Images.Add(modelImage);
                }

                return modelGallery;
        }
    }
}
