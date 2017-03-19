namespace avalonbuild.com.Models
{
	public class GalleryImage
	{
		public int GalleryID { get; set; }
		public int ImageID { get; set; }

		public Gallery Gallery { get; set; }
		public Image Image { get; set; }
	}
}