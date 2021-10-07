using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseItr.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MathTopics",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Геометрия" });

            migrationBuilder.InsertData(
                table: "MathTopics",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Арифметика" });

            migrationBuilder.InsertData(
                table: "MathTopics",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Алгебра" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MathTopics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MathTopics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MathTopics",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
