using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnimalAndMeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Animal_AnimalId",
                table: "Meal");

            migrationBuilder.DropIndex(
                name: "IX_Meal_AnimalId",
                table: "Meal");

            migrationBuilder.CreateTable(
                name: "AnimalMeal",
                columns: table => new
                {
                    AnimalId = table.Column<long>(type: "bigint", nullable: false),
                    MealsMealId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalMeal", x => new { x.AnimalId, x.MealsMealId });
                    table.ForeignKey(
                        name: "FK_AnimalMeal_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalMeal_Meal_MealsMealId",
                        column: x => x.MealsMealId,
                        principalTable: "Meal",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalMeal_MealsMealId",
                table: "AnimalMeal",
                column: "MealsMealId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalMeal");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_AnimalId",
                table: "Meal",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Animal_AnimalId",
                table: "Meal",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
