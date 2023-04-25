using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CSE_website.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CategoryParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CategoryParentId",
                        column: x => x.CategoryParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FacultyOffices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyOffices", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Outline = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Credit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partners_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TimeStartEvent = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TimeEndEvent = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    FacultyOfficeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_FacultyOffices_FacultyOfficeId",
                        column: x => x.FacultyOfficeId,
                        principalTable: "FacultyOffices",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    ResetTokenExpiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DepartmentSubject",
                columns: table => new
                {
                    DepartmentsId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentSubject", x => new { x.DepartmentsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_DepartmentSubject_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Gender = table.Column<string>(type: "enum('Nam','Nữ','Khác')", nullable: false),
                    AcademicRank = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    ResearchArea = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    LinkBlog = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Achievements = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    FacultyOfficeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lecturers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lecturers_FacultyOffices_FacultyOfficeId",
                        column: x => x.FacultyOfficeId,
                        principalTable: "FacultyOffices",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LecturerSubject",
                columns: table => new
                {
                    LecturersId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerSubject", x => new { x.LecturersId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_LecturerSubject_Lecturers_LecturersId",
                        column: x => x.LecturersId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PermissionId",
                table: "Accounts",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryParentId",
                table: "Categories",
                column: "CategoryParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyOfficeId",
                table: "Departments",
                column: "FacultyOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubject_SubjectsId",
                table: "DepartmentSubject",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_AccountId",
                table: "Lecturers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_DepartmentId",
                table: "Lecturers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_Email",
                table: "Lecturers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_FacultyOfficeId",
                table: "Lecturers",
                column: "FacultyOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_LinkBlog",
                table: "Lecturers",
                column: "LinkBlog",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_Phone",
                table: "Lecturers",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LecturerSubject_SubjectsId",
                table: "LecturerSubject",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_CategoryId",
                table: "Partners",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentSubject");

            migrationBuilder.DropTable(
                name: "LecturerSubject");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "FacultyOffices");
        }
    }
}
