using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeapYearApp.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "czy_przestepny",
                table: "LeapData",
                newName: "czy_przestepny");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "czy_przestepny",
                table: "LeapData",
                newName: "czy_przestepny");
        }
    }
}
