using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEFProject_DataAccess.Migrations
{
    public partial class addforbookpublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FluentBooks_Publisher_Id",
                table: "FluentBooks",
                column: "Publisher_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FluentBooks_FluentPublishers_Publisher_Id",
                table: "FluentBooks",
                column: "Publisher_Id",
                principalTable: "FluentPublishers",
                principalColumn: "Publisher_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FluentBooks_FluentPublishers_Publisher_Id",
                table: "FluentBooks");

            migrationBuilder.DropIndex(
                name: "IX_FluentBooks_Publisher_Id",
                table: "FluentBooks");
        }
    }
}
