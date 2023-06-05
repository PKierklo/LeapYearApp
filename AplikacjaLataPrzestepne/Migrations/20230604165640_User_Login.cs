using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeapYearApp.Migrations
{
    public partial class User_Login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "login",
                table: "LeapData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "LeapData",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "login",
                table: "LeapData");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "LeapData");
        }
    }
}
