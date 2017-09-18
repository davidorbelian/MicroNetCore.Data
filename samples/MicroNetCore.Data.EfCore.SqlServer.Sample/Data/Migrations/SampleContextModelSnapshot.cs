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

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.SampleClassOne", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.Property<long>("SampleClassTwoId");

                b.HasKey("Id");

                b.HasIndex("SampleClassTwoId");

                b.ToTable("SampleClassOne");
            });

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.SampleClassTwo", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("SampleClassTwo");
            });

            modelBuilder.Entity("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.SampleClassOne", b =>
            {
                b.HasOne("MicroNetCore.Data.EfCore.SqlServer.Sample.Models.SampleClassTwo", "SampleClassTwo")
                    .WithMany()
                    .HasForeignKey("SampleClassTwoId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}