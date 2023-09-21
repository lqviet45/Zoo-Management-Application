using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddingMeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_User_ZooTrainerId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_User_ZooTrainerId",
                table: "Experience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Experience",
                table: "Experience");

            migrationBuilder.DropIndex(
                name: "IX_Animal_ZooTrainerId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Experience");

            migrationBuilder.AlterColumn<long>(
                name: "AnimalId",
                table: "Experience",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "YearExp",
                table: "Experience",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Experience",
                table: "Experience",
                column: "ZooTrainerId");

            migrationBuilder.CreateTable(
                name: "AnimalUser",
                columns: table => new
                {
                    AnimalsAnimalId = table.Column<long>(type: "bigint", nullable: false),
                    ZooTrainersUserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalUser", x => new { x.AnimalsAnimalId, x.ZooTrainersUserId });
                    table.ForeignKey(
                        name: "FK_AnimalUser_Animal_AnimalsAnimalId",
                        column: x => x.AnimalsAnimalId,
                        principalTable: "Animal",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalUser_User_ZooTrainersUserId",
                        column: x => x.ZooTrainersUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ExperienceZooTrainerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skill_Experience_ExperienceZooTrainerId",
                        column: x => x.ExperienceZooTrainerId,
                        principalTable: "Experience",
                        principalColumn: "ZooTrainerId");
                });

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    MealId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<long>(type: "bigint", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedingTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.MealId);
                    table.ForeignKey(
                        name: "FK_Meal_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meal_FeedingFood_FoodId",
                        column: x => x.FoodId,
                        principalTable: "FeedingFood",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalUser_ZooTrainersUserId",
                table: "AnimalUser",
                column: "ZooTrainersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_AnimalId",
                table: "Meal",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_FoodId",
                table: "Meal",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_ExperienceZooTrainerId",
                table: "Skill",
                column: "ExperienceZooTrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_User_ZooTrainerId",
                table: "Experience",
                column: "ZooTrainerId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experience_User_ZooTrainerId",
                table: "Experience");

            migrationBuilder.DropTable(
                name: "AnimalUser");

            migrationBuilder.DropTable(
                name: "Meal");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "FeedingFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Experience",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "YearExp",
                table: "Experience");

            migrationBuilder.AlterColumn<long>(
                name: "AnimalId",
                table: "Experience",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Experience",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Experience",
                table: "Experience",
                columns: new[] { "ZooTrainerId", "AnimalId" });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_ZooTrainerId",
                table: "Animal",
                column: "ZooTrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_User_ZooTrainerId",
                table: "Animal",
                column: "ZooTrainerId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_User_ZooTrainerId",
                table: "Experience",
                column: "ZooTrainerId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
