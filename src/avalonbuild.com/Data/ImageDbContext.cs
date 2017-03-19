using Microsoft.EntityFrameworkCore;
using avalonbuild.com.Models;

namespace avalonbuild.com.Data
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GalleryImage>()
                .HasKey(gi => new { gi.GalleryID, gi.ImageID });

            modelBuilder.Entity<GalleryImage>()
                .HasOne(gi => gi.Gallery)
                .WithMany(g => g.Images)
                .HasForeignKey(gi => gi.GalleryID);

            modelBuilder.Entity<GalleryImage>()
                .HasOne(gi => gi.Image)
                .WithMany(i => i.Galleries)
                .HasForeignKey(gi => gi.ImageID);
        }
    }
}
