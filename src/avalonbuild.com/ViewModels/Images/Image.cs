using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace avalonbuild.com.ViewModels
{
	public class Image
	{
		public int ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Title")]
		public string Title { get; set; }

        [Display(Name = "Description")]
		public string Description { get; set; }

		public string FileName { get; set; }
		public string ThumbnailFileName { get; set; }
	}
}