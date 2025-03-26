using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trip.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSharedFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileURL",
                table: "SharedFiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "SharedFiles",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "SharedFiles");

            migrationBuilder.AddColumn<string>(
                name: "FileURL",
                table: "SharedFiles",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
