using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEFProject_DataAccess.Migrations
{
    public partial class ChangeNameToGenreNameTblBenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           // migrationBuilder.Sql("Update dbo.Genres SET GenreName=Name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "GenreName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genres",
                newName: "Name");
        }
    }
}
