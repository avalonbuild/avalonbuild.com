using Microsoft.EntityFrameworkCore;
using avalonbuild.com.Models;

namespace avalonbuild.com.Data
{
    public class FileDbContext : DbContext
    {
        public FileDbContext(DbContextOptions<FileDbContext> options)
            : base(options)
        {
        }

        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>().ToTable("File");
        }
    }
}
