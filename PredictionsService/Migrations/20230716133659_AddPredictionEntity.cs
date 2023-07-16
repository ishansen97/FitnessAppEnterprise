using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PredictionsService.Migrations
{
    public partial class AddPredictionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Predictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    CurrentWeight = table.Column<double>(type: "float", nullable: false),
                    PredictedWeight = table.Column<double>(type: "float", nullable: false),
                    BodyFatPercentage = table.Column<double>(type: "float", nullable: false),
                    PredictedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BMI = table.Column<double>(type: "float", nullable: false),
                    PredictedBMI = table.Column<double>(type: "float", nullable: false),
                    WeightStatus = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Predictions");
        }
    }
}
