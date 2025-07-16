using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEFProject_DataAccess.Migrations
{
    public partial class addforbookfluent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FluentBooks_BookDetail_Id",
                table: "FluentBooks",
                column: "BookDetail_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FluentBooks_FluentBookDetails_BookDetail_Id",
                table: "FluentBooks",
                column: "BookDetail_Id",
                principalTable: "FluentBookDetails",
                principalColumn: "BookDetail_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FluentBooks_FluentBookDetails_BookDetail_Id",
                table: "FluentBooks");

            migrationBuilder.DropIndex(
                name: "IX_FluentBooks_BookDetail_Id",
                table: "FluentBooks");
        }
    }
}
