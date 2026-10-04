using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAvailableColumnToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Copies");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Copies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Copies");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Copies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
