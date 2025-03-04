using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrostFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBTUType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnergyEfficiency",
                table: "AirConditioners");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AirConditioners");

            migrationBuilder.DropColumn(
                name: "NoiseLevel",
                table: "AirConditioners");

            migrationBuilder.DropColumn(
                name: "RecommendedRoomSize",
                table: "AirConditioners");

            migrationBuilder.AlterColumn<int>(
                name: "CoolingCapacity",
                table: "AirConditioners",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    BTU = table.Column<double>(type: "float", nullable: false),
                    RecommendedACId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_AirConditioners_RecommendedACId",
                        column: x => x.RecommendedACId,
                        principalTable: "AirConditioners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RecommendedACId",
                table: "Rooms",
                column: "RecommendedACId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.AlterColumn<double>(
                name: "CoolingCapacity",
                table: "AirConditioners",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "EnergyEfficiency",
                table: "AirConditioners",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AirConditioners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "NoiseLevel",
                table: "AirConditioners",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedRoomSize",
                table: "AirConditioners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
