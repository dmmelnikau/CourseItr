using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseItr.Migrations
{
    public partial class Ratin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "RatingModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingModels_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateTable(
                name: "MTaskRatingModel",
                columns: table => new
                {
                    MTasksId = table.Column<int>(type: "int", nullable: false),
                    RatingModelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MTaskRatingModel", x => new { x.MTasksId, x.RatingModelsId });
                    table.ForeignKey(
                        name: "FK_MTaskRatingModel_MTasks_MTasksId",
                        column: x => x.MTasksId,
                        principalTable: "MTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MTaskRatingModel_RatingModels_RatingModelsId",
                        column: x => x.RatingModelsId,
                        principalTable: "RatingModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "MTaskRatingModel");



            migrationBuilder.DropTable(
                name: "RatingModels");

        }
    }
}
