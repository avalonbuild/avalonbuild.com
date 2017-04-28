using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using avalonbuild.com.Data;

namespace avalonbuild.com.Migrations.ImageDb
{
    [DbContext(typeof(ImageDbContext))]
    [Migration("20170329160412_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("avalonbuild.com.Models.Gallery", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("avalonbuild.com.Models.GalleryImage", b =>
                {
                    b.Property<int>("GalleryID");

                    b.Property<int>("ImageID");

                    b.HasKey("GalleryID", "ImageID");

                    b.HasIndex("ImageID");

                    b.ToTable("GalleryImage");
                });

            modelBuilder.Entity("avalonbuild.com.Models.Image", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("FileName");

                    b.Property<string>("ThumbnailFileName");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("avalonbuild.com.Models.GalleryImage", b =>
                {
                    b.HasOne("avalonbuild.com.Models.Gallery", "Gallery")
                        .WithMany("Images")
                        .HasForeignKey("GalleryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("avalonbuild.com.Models.Image", "Image")
                        .WithMany("Galleries")
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
