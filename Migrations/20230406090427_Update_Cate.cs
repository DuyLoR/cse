using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSE_website.Migrations
{
    /// <inheritdoc />
    public partial class Update_Cate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LecturerId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Categories",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "Categories",
                type: "enum('Không','Trang hiện tại','Trang mới')",
                nullable: false,
                defaultValue: "Không");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Categories",
                type: "enum('1','2')",
                nullable: false,
                defaultValue: "1");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LecturerId",
                table: "Categories",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Lecturers_LecturerId",
                table: "Categories",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Lecturers_LecturerId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LecturerId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Categories");
        }
    }
}
