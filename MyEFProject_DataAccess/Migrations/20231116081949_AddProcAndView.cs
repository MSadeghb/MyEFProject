using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEFProject_DataAccess.Migrations
{
    public partial class AddProcAndView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW dbo.GetOnlyBookDetails
             AS
             SELECT m.ISBN , m.Title ,m.Price From dbo.Books m");

            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.getAllBookDetails 
                                     @bookId int
                                     AS
                                    SET NOCOUNT ON ;
                                    SELECT * From dbo.Books b 
                                    Where b.Book_Id=@bookId
                                    GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.GetOnlyBookDetails");
            migrationBuilder.Sql("DROP PROCEDURE dbo.getAllBookDetails");
        }
    }
}
