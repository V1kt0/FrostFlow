using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrostFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCoolingCapacityToAirConditioner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CoolingCapacity",
                table: "AirConditioners",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoolingCapacity",
                table: "AirConditioners");
        }
    }
}
