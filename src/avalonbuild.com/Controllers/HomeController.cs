using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using avalonbuild.com.Models;
using avalonbuild.com.Models.Galleries;

namespace avalonbuild.com.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/services")]
        public IActionResult Services()
        {
            return View();
        }

        [Route("/galleries")]
        public IActionResult Galleries()
        {
            List<Gallery> galleries = new List<Gallery>()
            {
                new Gallery {Name="Interiors", Url= "/gallery/interiors", ImageUrl="~/images/galleries/interiors.jpg"},
                new Gallery {Name="Exteriors", Url= "/gallery/exteriors", ImageUrl="~/images/galleries/exteriors.jpg"},
                new Gallery {Name="Details", Url= "/gallery/details", ImageUrl="~/images/galleries/details.jpg"},
                new Gallery {Name="Before and After", Url= "/gallery/before-and-after", ImageUrl="~/images/galleries/before-and-after.jpg"}
            };

            return View(galleries);
        }

        [Route("/gallery/{name}")]
        public IActionResult Gallery(string name)
        {
            Gallery gallery = new Gallery()
            {
                Name = name,
                Url = "/gallery/" + name,
                Images = new List<Image>()
                {
                    new Image {Name="One", ImageUrl="~/images/gallery/b25-1.JPG"},
                    new Image {Name="Two", ImageUrl="~/images/gallery/b38.JPG"},
                    new Image {Name="Three", ImageUrl="~/images/gallery/b47.JPG"},
                    new Image {Name="Four", ImageUrl="~/images/gallery/b55.JPG"},
                    new Image {Name="Five", ImageUrl="~/images/gallery/c6.JPG"},
                    new Image {Name="Six", ImageUrl="~/images/gallery/c33.JPG"}
                }
            };

            return View(gallery);
        }

        [Route("/referrals")]
        public IActionResult Referrals()
        {
            return View();
        }

    }
}
