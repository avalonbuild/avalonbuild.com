using System.Collections.Generic;

namespace avalonbuild.com.ViewModels
{
	public class Gallery
	{
		public Gallery()
		{
			Images = new List<Image>();
		}

		public int ID { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public List<Image> Images { get; set; }
	}
}