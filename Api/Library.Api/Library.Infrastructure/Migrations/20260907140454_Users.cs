using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Library.Application.Common.Interfaces.IAppDbContext.Users",
                table: "Library.Application.Common.Interfaces.IAppDbContext.Users");

            migrationBuilder.RenameTable(
                name: "Library.Application.Common.Interfaces.IAppDbContext.Users",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Library.Application.Common.Interfaces.IAppDbContext.Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library.Application.Common.Interfaces.IAppDbContext.Users",
                table: "Library.Application.Common.Interfaces.IAppDbContext.Users",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);
        }
    }
}
