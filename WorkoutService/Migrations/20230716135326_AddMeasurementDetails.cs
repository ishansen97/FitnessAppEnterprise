using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutService.Migrations
{
    public partial class AddMeasurementDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutType = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseMeasurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealType = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealMeasurements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMeasurements_WorkoutType",
                table: "ExerciseMeasurements",
                column: "WorkoutType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealMeasurements_MealType",
                table: "MealMeasurements",
                column: "MealType",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseMeasurements");

            migrationBuilder.DropTable(
                name: "MealMeasurements");
        }
    }
}
