using System.Collections.Generic;

namespace avalonbuild.com.Models.Galleries
{
	public class Gallery
	{
		public Gallery()
		{
			Images = new List<Image>();
		}

		public string ID { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public string ImageUrl { get; set; }
		public List<Image> Images { get; set;}
	}
}