using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTime.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddArtistDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DebutYear",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FansCount",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrending",
                table: "Artists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Artists",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TrendingReason",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YouTube",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "DebutYear",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "FansCount",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "IsTrending",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "TrendingReason",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "YouTube",
                table: "Artists");
        }
    }
}
