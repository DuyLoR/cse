﻿// <auto-generated />
using System;
using CSE_website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CSE_website.Migrations
{
    [DbContext(typeof(CSEWebsiteContext))]
    [Migration("20230406090427_Update_Cate")]
    partial class Update_Cate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CSE_website.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("PermissionId")
                        .HasColumnType("int");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ResetTokenExpiration")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CSE_website.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CategoryParentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("LecturerId")
                        .HasColumnType("int");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasColumnType("enum('Không','Trang hiện tại','Trang mới')");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("enum(1,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryParentId");

                    b.HasIndex("LecturerId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CSE_website.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Decription")
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("FacultyOfficeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyOfficeId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CSE_website.Models.FacultyOffice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("FacultyOffices");
                });

            modelBuilder.Entity("CSE_website.Models.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AcademicRank")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Achievements")
                        .HasColumnType("text");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("FacultyOfficeId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("enum('Nam','Nữ','Khác')");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LinkBlog")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ResearchArea")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("FacultyOfficeId");

                    b.HasIndex("LinkBlog")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("CSE_website.Models.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("CSE_website.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("CSE_website.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("TimeEndEvent")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("TimeStartEvent")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("CSE_website.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Credit")
                        .HasColumnType("int");

                    b.Property<string>("Decription")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Outline")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("DepartmentSubject", b =>
                {
                    b.Property<int>("DepartmentsId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentsId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("DepartmentSubject");
                });

            modelBuilder.Entity("LecturerSubject", b =>
                {
                    b.Property<int>("LecturersId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("LecturersId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("LecturerSubject");
                });

            modelBuilder.Entity("CSE_website.Models.Account", b =>
                {
                    b.HasOne("CSE_website.Models.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId");

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("CSE_website.Models.Category", b =>
                {
                    b.HasOne("CSE_website.Models.Category", "CategoryParent")
                        .WithMany()
                        .HasForeignKey("CategoryParentId");

                    b.HasOne("CSE_website.Models.Lecturer", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId");

                    b.Navigation("CategoryParent");

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("CSE_website.Models.Department", b =>
                {
                    b.HasOne("CSE_website.Models.FacultyOffice", "FacultyOffice")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyOfficeId");

                    b.Navigation("FacultyOffice");
                });

            modelBuilder.Entity("CSE_website.Models.Lecturer", b =>
                {
                    b.HasOne("CSE_website.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("CSE_website.Models.Department", "Department")
                        .WithMany("Lecturers")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("CSE_website.Models.FacultyOffice", "FacultyOffice")
                        .WithMany("Lecturers")
                        .HasForeignKey("FacultyOfficeId");

                    b.Navigation("Account");

                    b.Navigation("Department");

                    b.Navigation("FacultyOffice");
                });

            modelBuilder.Entity("CSE_website.Models.Partner", b =>
                {
                    b.HasOne("CSE_website.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CSE_website.Models.Post", b =>
                {
                    b.HasOne("CSE_website.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DepartmentSubject", b =>
                {
                    b.HasOne("CSE_website.Models.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSE_website.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LecturerSubject", b =>
                {
                    b.HasOne("CSE_website.Models.Lecturer", null)
                        .WithMany()
                        .HasForeignKey("LecturersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSE_website.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CSE_website.Models.Department", b =>
                {
                    b.Navigation("Lecturers");
                });

            modelBuilder.Entity("CSE_website.Models.FacultyOffice", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Lecturers");
                });
#pragma warning restore 612, 618
        }
    }
}
