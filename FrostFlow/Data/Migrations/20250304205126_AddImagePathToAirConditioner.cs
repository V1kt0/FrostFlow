using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrostFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToAirConditioner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AirConditioners_RecommendedACId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RecommendedACId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RecommendedACId",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "AirConditioners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usage",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AirConditioners");

            migrationBuilder.AddColumn<int>(
                name: "RecommendedACId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RecommendedACId",
                table: "Rooms",
                column: "RecommendedACId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AirConditioners_RecommendedACId",
                table: "Rooms",
                column: "RecommendedACId",
                principalTable: "AirConditioners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
