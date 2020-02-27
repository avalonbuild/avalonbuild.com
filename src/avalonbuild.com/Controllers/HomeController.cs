using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using avalonbuild.com.Data;
using avalonbuild.com.Models;

namespace avalonbuild.com.Controllers
{
    public class HomeController : Controller
    {
        private readonly ImageDbContext _images;

        public HomeController(ImageDbContext images)
        {
            _images = images;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/galleries")]
        public async Task<IActionResult> Galleries()
        {
            var galleries = await _images.Galleries.Include(g => g.Images).ThenInclude(i => i.Image).ToListAsync();

            var model = new List<ViewModels.Gallery>();

            foreach (var gallery in galleries)
            {
                gallery.Images = gallery.Images.OrderByDescending(i => i.ImageID).ToList();

                model.Add(GalleryModelToViewModel(gallery));
            }

            return View(model);
        }

        [Route("/gallery/{name}")]
        public async Task<IActionResult> Gallery(string name)
        {
            var gallery = await _images.Galleries.Include(g => g.Images).ThenInclude(i => i.Image).FirstOrDefaultAsync(i => i.Name == name);

            if (gallery == null)
                return NotFound();

            gallery.Images = gallery.Images.OrderByDescending(i => i.ImageID).ToList();

            return View(GalleryModelToViewModel(gallery));
        }

        [Route("/referrals")]
        public IActionResult Referrals()
        {
            return View();
        }

        [Route("/available-homes")]
        public IActionResult AvailableHomes()
        {
            return View();
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
                        FileName = image.Image.FileName,
                        ThumbnailFileName = image.Image.ThumbnailFileName
                    };

                    modelGallery.Images.Add(modelImage);
                }

                return modelGallery;
        }

    }
}
