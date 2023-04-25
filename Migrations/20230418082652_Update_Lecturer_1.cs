using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSE_website.Migrations
{
    /// <inheritdoc />
    public partial class Update_Lecturer_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicRank",
                table: "Lecturers");

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "Lecturers",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rank",
                table: "Lecturers",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LecturerCode",
                table: "Lecturers",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_LecturerCode",
                table: "Lecturers",
                column: "LecturerCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lecturers_LecturerCode",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "LecturerCode",
                table: "Lecturers");
        }
    }
}
