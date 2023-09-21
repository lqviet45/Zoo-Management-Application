using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Removekey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Animal_AnimalId",
                table: "Experience");

            migrationBuilder.DropIndex(
                name: "IX_Experience_AnimalId",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Experience");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AnimalId",
                table: "Experience",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experience_AnimalId",
                table: "Experience",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Animal_AnimalId",
                table: "Experience",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "AnimalId");
        }
    }
}
