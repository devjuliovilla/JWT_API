﻿// <auto-generated />
using JWT_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JWT_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240122185604_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JWT_API.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("NAME");

                    b.HasKey("Id");

                    b.ToTable("Roles", "dbo");
                });

            modelBuilder.Entity("JWT_API.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)")
                        .HasColumnName("PASS");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("ROLEID");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("USNAME");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", "dbo");
                });

            modelBuilder.Entity("JWT_API.Entities.User", b =>
                {
                    b.HasOne("JWT_API.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("JWT_API.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
