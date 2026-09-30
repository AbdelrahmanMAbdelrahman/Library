using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BorrowigRecordOneToManyCopie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BorrowingRecords_CopyId",
                table: "BorrowingRecords");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_CopyId",
                table: "BorrowingRecords",
                column: "CopyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BorrowingRecords_CopyId",
                table: "BorrowingRecords");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_CopyId",
                table: "BorrowingRecords",
                column: "CopyId",
                unique: true);
        }
    }
}
