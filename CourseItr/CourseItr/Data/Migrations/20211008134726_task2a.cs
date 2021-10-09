using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseItr.Migrations
{
    public partial class task2a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Condotion",
                table: "MathTasks",
                newName: "Condition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "MathTasks",
                newName: "Condotion");
        }
    }
}
