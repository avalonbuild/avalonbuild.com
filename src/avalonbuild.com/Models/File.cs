using System.ComponentModel.DataAnnotations;

namespace avalonbuild.com.Models
{
	public class File
	{
		[Key]
		public string Name { get; set; }
		public string MimeType { get; set; }
		public byte[] Data { get; set; }
	}
}