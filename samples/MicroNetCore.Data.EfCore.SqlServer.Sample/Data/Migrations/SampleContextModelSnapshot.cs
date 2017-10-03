﻿// <auto-generated />

using MicroNetCore.Data.EfCore.SqlServer.Sample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Migrations
{
    [DbContext(typeof(SampleContext))]
    internal class SampleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.City", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("City");
            });

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.Role", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("Role");
            });

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.User", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("User");
            });

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.UserCity", b =>
            {
                b.Property<long>("Entity1Id");

                b.Property<long>("Entity2Id");

                b.HasKey("Entity1Id", "Entity2Id");

                b.HasIndex("Entity2Id");

                b.ToTable("UserCity");
            });

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.UserRole", b =>
            {
                b.Property<long>("Entity1Id");

                b.Property<long>("Entity2Id");

                b.HasKey("Entity1Id", "Entity2Id");

                b.HasIndex("Entity2Id");

                b.ToTable("UserRole");
            });

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.UserCity", b =>
            {
                b.HasOne("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.User", "Entity1")
                    .WithMany("Cities")
                    .HasForeignKey("Entity1Id")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.City", "Entity2")
                    .WithMany("Users")
                    .HasForeignKey("Entity2Id")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.UserRole", b =>
            {
                b.HasOne("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.User", "Entity1")
                    .WithMany("Roles")
                    .HasForeignKey("Entity1Id")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.Role", "Entity2")
                    .WithMany("Users")
                    .HasForeignKey("Entity2Id")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}