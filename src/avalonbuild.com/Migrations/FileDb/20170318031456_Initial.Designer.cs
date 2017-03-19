using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using avalonbuild.com.Data;

namespace avalonbuild.com.Migrations.FileDb
{
    [DbContext(typeof(FileDbContext))]
    [Migration("20170318031456_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("avalonbuild.com.Models.File", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Data");

                    b.Property<string>("MimeType");

                    b.HasKey("Name");

                    b.ToTable("File");
                });
        }
    }
}
