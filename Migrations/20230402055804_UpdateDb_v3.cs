using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSE_website.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Partners",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Partners",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Lecturers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lecturers");
        }
    }
}
