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
        public async Task<IActionResult> Get()
        {
            var galleries = await _images.Galleries.Include(g => g.Images).ThenInclude(i => i.Image).ToListAsync();

            var model = new List<ViewModels.Gallery>();

            foreach (var gallery in galleries)
            {
                model.Add(GalleryModelToViewModel(gallery));
            }

            return Ok(model);
        }

        [HttpGet("{id}", Name = "GetGallery")]
        public async Task<IActionResult> Get(int id)
        {
            var gallery = await _images.Galleries.Include(g => g.Images).ThenInclude(i => i.Image).SingleOrDefaultAsync(i => i.ID == id);

            if (gallery == null)
                return NotFound();

            return Ok(GalleryModelToViewModel(gallery));
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViewModels.Gallery gallery)
        {
            if (gallery == null)
                return BadRequest("Gallery information required.");

            var dbGallery = new Models.Gallery{
                Name = gallery.Name,
                Title = gallery.Title,
                Description = gallery.Description
            };

            try
            {
                _images.Galleries.Add(dbGallery);

                foreach (var image in gallery.Images)
                {
                    var dbImage = await _images.Images.SingleOrDefaultAsync(i => i.ID == image.ID);

                    if (dbImage != null)
                        dbGallery.Images.Add(new GalleryImage { Image = dbImage});
                }

                await _images.SaveChangesAsync();

                return CreatedAtRoute("GetGallery", new { id = dbGallery.ID }, GalleryModelToViewModel(dbGallery));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ViewModels.Gallery gallery)
        {
            if (gallery == null)
                return BadRequest("Error updating gallery.");

            var dbGallery = await _images.Galleries.Include(g => g.Images).ThenInclude(i => i.Image).SingleOrDefaultAsync(i => i.ID == id);

            if (dbGallery == null)
                return NotFound();

            try
            {
                dbGallery.ID = id;
                dbGallery.Name = gallery.Name;
                dbGallery.Title = gallery.Title;
                dbGallery.Description = gallery.Description;

                _images.Galleries.Update(dbGallery);

                //remove images that are not in the new list
                foreach (var image in dbGallery.Images.ToArray())
                {
                    bool remove = true;

                    foreach (var i in gallery.Images)
                    {
                        if (i.ID == image.ImageID)
                            remove = false;
                    }

                    if (remove)
                        dbGallery.Images.Remove(image);
                }

                // add images from the new list that are not in the old list
                foreach (var image in gallery.Images)
                {
                    bool add = true;

                    foreach (var i in dbGallery.Images)
                    {
                        if (i.ImageID == image.ID)
                            add = false;
                    }

                    if (add)
                    {
                        var dbImage = await _images.Images.SingleOrDefaultAsync(i => i.ID == image.ID);

                        if (dbImage != null)
                            dbGallery.Images.Add(new GalleryImage { Image = dbImage});
                    }
                }

                await _images.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var gallery = await _images.Galleries.Include(g => g.Images).FirstOrDefaultAsync(i => i.ID == id);

            if (gallery == null)
                return NotFound();

            try
            {
                _images.Galleries.Remove(gallery);

                await _images.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
                    if (image.Image != null) {

                        var modelImage = new ViewModels.Image {
                            ID = image.Image.ID,
                            Name = image.Image.Name,
                            Title = image.Image.Title,
                            Description = image.Image.Description,
                            FileName = image.Image.FileName,
                            ThumbnailFileName = image.Image.ThumbnailFileName
                        };

                        modelGallery.Images.Add(modelImage);
                    }
                }

                return modelGallery;
        }
    }
}
