using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSE_website.Migrations
{
    /// <inheritdoc />
    public partial class Update_Cate_Relations_And_Enum_list : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryParentId",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Target",
                table: "Categories",
                type: "enum('_self','_blank')",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Không','Trang hiện tại','Trang mới')");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryParentId",
                table: "Categories",
                column: "CategoryParentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryParentId",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Target",
                table: "Categories",
                type: "enum('Không','Trang hiện tại','Trang mới')",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('_self','_blank')");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryParentId",
                table: "Categories",
                column: "CategoryParentId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
