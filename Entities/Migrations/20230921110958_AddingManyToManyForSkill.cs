using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddingManyToManyForSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Experience_ExperienceZooTrainerId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_ExperienceZooTrainerId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "ExperienceZooTrainerId",
                table: "Skill");

            migrationBuilder.CreateTable(
                name: "ExperienceSkill",
                columns: table => new
                {
                    ExperiencesZooTrainerId = table.Column<long>(type: "bigint", nullable: false),
                    SkillsSkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceSkill", x => new { x.ExperiencesZooTrainerId, x.SkillsSkillId });
                    table.ForeignKey(
                        name: "FK_ExperienceSkill_Experience_ExperiencesZooTrainerId",
                        column: x => x.ExperiencesZooTrainerId,
                        principalTable: "Experience",
                        principalColumn: "ZooTrainerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienceSkill_Skill_SkillsSkillId",
                        column: x => x.SkillsSkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceSkill_SkillsSkillId",
                table: "ExperienceSkill",
                column: "SkillsSkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperienceSkill");

            migrationBuilder.AddColumn<long>(
                name: "ExperienceZooTrainerId",
                table: "Skill",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_ExperienceZooTrainerId",
                table: "Skill",
                column: "ExperienceZooTrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Experience_ExperienceZooTrainerId",
                table: "Skill",
                column: "ExperienceZooTrainerId",
                principalTable: "Experience",
                principalColumn: "ZooTrainerId");
        }
    }
}
