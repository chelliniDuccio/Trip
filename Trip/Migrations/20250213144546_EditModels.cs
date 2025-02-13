using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trip.Migrations
{
    /// <inheritdoc />
    public partial class EditModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "UsefulLinks",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "UsefulLinks",
                newName: "CreationAt");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Travels",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Travels",
                newName: "CreationAt");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "SharedFiles",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "SharedFiles",
                newName: "CreationAt");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Expenses",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Expenses",
                newName: "CreationAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "UsefulLinks",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreationAt",
                table: "UsefulLinks",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Travels",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreationAt",
                table: "Travels",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "SharedFiles",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreationAt",
                table: "SharedFiles",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Expenses",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreationAt",
                table: "Expenses",
                newName: "CreationDate");
        }
    }
}
