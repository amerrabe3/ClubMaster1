using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubMaster3.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Coach",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Coach");
        }
    }
}
