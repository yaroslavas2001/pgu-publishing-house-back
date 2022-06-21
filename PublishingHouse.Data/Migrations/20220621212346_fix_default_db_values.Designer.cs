﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublishingHouse.Data;

#nullable disable

namespace PublishingHouse.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220621212346_fix_default_db_values")]
    partial class fix_default_db_values
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PublishingHouse.Data.Models.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int?>("AcademicDegree")
                        .HasColumnType("int");

                    b.Property<string>("Contacts")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DepartmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeerPosition")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTeacher")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NonStuffPosition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NonStuffWorkPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("FacultyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Faculty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("IsVisibleForReviewers")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PublicationId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ReviewId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.HasIndex("ReviewId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.MailToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DateExpire")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Key")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MailToken");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Publication", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ReviewerId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UDC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ReviewerId");

                    b.HasIndex("UserId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.PublicationAuthors", b =>
                {
                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<long>("PublicationId")
                        .HasColumnType("bigint");

                    b.HasKey("AuthorId", "PublicationId");

                    b.HasIndex("PublicationId");

                    b.ToTable("PublicationsAuthors");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PublicationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Reviewer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviewers");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("SureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Author", b =>
                {
                    b.HasOne("PublishingHouse.Data.Models.Department", "Department")
                        .WithMany("Authors")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Department", b =>
                {
                    b.HasOne("PublishingHouse.Data.Models.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.File", b =>
                {
                    b.HasOne("PublishingHouse.Data.Models.Publication", "Publication")
                        .WithMany("Files")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PublishingHouse.Data.Models.Review", "Review")
                        .WithMany("Files")
                        .HasForeignKey("ReviewId");

                    b.Navigation("Publication");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.MailToken", b =>
                {
                    b.HasOne("PublishingHouse.Data.Models.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Publication", b =>
                {
                    b.HasOne("PublishingHouse.Data.Models.Reviewer", "Reviewer")
                        .WithMany("Publications")
                        .HasForeignKey("ReviewerId");

                    b.HasOne("PublishingHouse.Data.Models.User", "User")
                        .WithMany("Publications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reviewer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.PublicationAuthors", b =>
                {
                    b.HasOne("PublishingHouse.Data.Models.Publication", "Publication")
                        .WithMany("Authors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PublishingHouse.Data.Models.Author", "Author")
                        .WithMany("Publications")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Review", b =>
                {
                    b.HasOne("PublishingHouse.Data.Models.Publication", "Publication")
                        .WithMany("Reviews")
                        .HasForeignKey("PublicationId");

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Author", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Department", b =>
                {
                    b.Navigation("Authors");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Faculty", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Publication", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Files");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Review", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.Reviewer", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("PublishingHouse.Data.Models.User", b =>
                {
                    b.Navigation("Publications");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
