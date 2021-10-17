using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseItr.Migrations
{
    public partial class RataingTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatingTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MTaskId = table.Column<int>(type: "int", nullable: false),
                    RatingModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingTask_MTasks_MTaskId",
                        column: x => x.MTaskId,
                        principalTable: "MTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingTask_RatingModels_RatingModelId",
                        column: x => x.RatingModelId,
                        principalTable: "RatingModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingTask_MTaskId",
                table: "RatingTask",
                column: "MTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTask_RatingModelId",
                table: "RatingTask",
                column: "RatingModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingTask");
        }
    }
}
