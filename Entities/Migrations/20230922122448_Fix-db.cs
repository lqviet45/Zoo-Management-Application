using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Fixdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_User_ZooTrainerId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_ZooTrainerId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "ZooTrainerId",
                table: "Experiences");

            migrationBuilder.AddColumn<int>(
                name: "ExperienceId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ExperienceId",
                table: "User",
                column: "ExperienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Experiences_ExperienceId",
                table: "User",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "ExperienceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Experiences_ExperienceId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ExperienceId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ExperienceId",
                table: "User");

            migrationBuilder.AddColumn<long>(
                name: "ZooTrainerId",
                table: "Experiences",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ZooTrainerId",
                table: "Experiences",
                column: "ZooTrainerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_User_ZooTrainerId",
                table: "Experiences",
                column: "ZooTrainerId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
