using System.Collections.Generic;

namespace avalonbuild.com.Models
{
	public class Gallery
	{
		public Gallery()
		{
			Images = new List<GalleryImage>();
		}

		public int ID { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string FileName { get; set; }
		public List<GalleryImage> Images { get; set; }
	}
}