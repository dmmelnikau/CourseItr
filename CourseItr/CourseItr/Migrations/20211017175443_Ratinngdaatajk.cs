using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseItr.Migrations
{
    public partial class Ratinngdaatajk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VanityUrl",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VanityUrl",
                table: "AspNetUsers");
        }
    }
}
