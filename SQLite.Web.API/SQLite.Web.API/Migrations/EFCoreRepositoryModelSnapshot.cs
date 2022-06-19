﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SQLite.Web.API.Services;

#nullable disable

namespace SQLite.Web.API.Migrations
{
    [DbContext(typeof(EFCoreRepository))]
    partial class EFCoreRepositoryModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("SQLite.Web.API.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayName = "User 1",
                            Email = "user_1@somewhere.com"
                        },
                        new
                        {
                            Id = 2,
                            DisplayName = "User 2",
                            Email = "user_2@somewhere.com"
                        },
                        new
                        {
                            Id = 3,
                            DisplayName = "User 3",
                            Email = "user_3@somewhere.com"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
