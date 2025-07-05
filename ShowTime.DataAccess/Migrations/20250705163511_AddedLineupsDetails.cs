using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTime.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedLineupsDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lineups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLivePerformance",
                table: "Lineups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainStage",
                table: "Lineups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StageTheme",
                table: "Lineups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasAfterParty",
                table: "Festivals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCamping",
                table: "Festivals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFoodCourt",
                table: "Festivals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIndoor",
                table: "Festivals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Festivals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Festivals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "IsLivePerformance",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "IsMainStage",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "StageTheme",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "HasAfterParty",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "HasCamping",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "HasFoodCourt",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "IsIndoor",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Festivals");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Festivals");
        }
    }
}
