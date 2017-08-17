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
using ImageSharp;

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
            var images = await _images.Images.ToListAsync();

            return Ok(images);
        }

        [HttpGet("{id}", Name = "GetImage")]
        public async Task<IActionResult> Get(int id)
        {
            var image = await _images.Images.SingleOrDefaultAsync(i => i.ID == id);

            if (image == null)
                return NotFound();

            return Ok(image);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Image model)
        {
            if (!ModelState.IsValid || Request.Form.Files.Count != 1)
                return BadRequest("Image file is required.");

            if (model.Name == null || model.Name == "")
                model.Name = Request.Form.Files[0].FileName;    //filename is optional, if not provided use the uploaded file name

            if (await FileExists("images/" + model.Name))
                return StatusCode(StatusCodes.Status409Conflict, "An image with the same filename already exists.");

            using (var memoryStream = new MemoryStream())
            {
                await Request.Form.Files[0].CopyToAsync(memoryStream);

                try {
                    await SaveImageFile(memoryStream, "images/" + model.Name, Request.Form.Files[0].ContentType);
                    await SaveImageThumbnailFile(memoryStream, "images/thumb-" + model.Name, Request.Form.Files[0].ContentType);
                    var image = await SaveImage(model.Name, model.Title, model.Description, "images/" + model.Name, "images/thumb-" + model.Name);

                    return CreatedAtRoute("GetImage", new { id = image.ID }, image);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _images.Images.SingleOrDefaultAsync(i => i.ID == id);

            if (image == null)
                return NotFound();

            try {
                var file = new Models.File {
                    Name = image.FileName
                };

                var thumbfile = new Models.File {
                    Name = image.ThumbnailFileName
                };

                _files.Attach(file);
                _files.Remove(file);

                _files.Attach(thumbfile);
                _files.Remove(thumbfile);

                await _files.SaveChangesAsync();

                _images.Remove(image);
                await _images.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private async Task<bool> FileExists(string FileName)
        {
            return await _files.Files.AnyAsync(i => i.Name == FileName);
        }

        private async Task SaveImageFile(MemoryStream FileStream, string FileName, string ContentType)
        {
            var maxWidth = 1920;

            var file = new Models.File
            {
                Name = FileName,
                MimeType = ContentType
            };

            try {
                using (var resizedImage = ImageSharp.Image.Load(FileStream.ToArray()))
                {
                    using (var resizeStream = new MemoryStream()) {

                        resizedImage.Resize(maxWidth, (resizedImage.Height / resizedImage.Width) * maxWidth).Save(resizeStream, ImageFormats.Jpeg);
                        file.Data = resizeStream.ToArray();
                    }
                }

                _files.Files.Add(file);

                await _files.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error saving image file.");
            }
        }

        private async Task SaveImageThumbnailFile(MemoryStream FileStream, string FileName, string ContentType)
        {
            var maxThumbWidth = 616;

            var file = new Models.File
            {
                Name = FileName,
                MimeType = ContentType
            };

            try {
                using (var resizedImage = ImageSharp.Image.Load(FileStream.ToArray()))
                {
                    using (var resizeStream = new MemoryStream()) {

                        resizedImage.Resize(maxThumbWidth, (resizedImage.Height / resizedImage.Width) * maxThumbWidth).Save(resizeStream, ImageFormats.Jpeg);
                        file.Data = resizeStream.ToArray();
                    }
                }

                _files.Files.Add(file);

                await _files.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error saving thumbnail image file.");
            }
        }

        private async Task<Models.Image> SaveImage(string Name, string Title, string Description, string FileName, string ThumbnailFileName)
        {
            var image = new Models.Image
            {
                Name = Name,
                Title = Title,
                Description = Description,
                FileName = FileName,
                ThumbnailFileName = ThumbnailFileName
            };

            try {

                _images.Images.Add(image);

                await _images.SaveChangesAsync();

                return image;
            }
            catch (Exception)
            {
                throw new Exception("Error saving image.");
            }
        }
    }
}
