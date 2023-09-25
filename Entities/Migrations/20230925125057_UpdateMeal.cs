using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meal_FeedingFood_FoodId",
                table: "Meal");

            migrationBuilder.DropTable(
                name: "FeedingFood");

            migrationBuilder.DropIndex(
                name: "IX_Meal_FoodId",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Meal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodId",
                table: "Meal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FeedingFood",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingFood", x => x.FoodId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meal_FoodId",
                table: "Meal",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_FeedingFood_FoodId",
                table: "Meal",
                column: "FoodId",
                principalTable: "FeedingFood",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
