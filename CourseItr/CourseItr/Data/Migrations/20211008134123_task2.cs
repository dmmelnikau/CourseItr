using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseItr.Migrations
{
    public partial class task2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Condotion",
                table: "MathTasks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condotion",
                table: "MathTasks");
        }
    }
}
