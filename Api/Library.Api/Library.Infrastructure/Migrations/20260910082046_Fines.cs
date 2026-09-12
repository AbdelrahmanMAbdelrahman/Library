using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BorrowingRecords_AppUserId",
                table: "BorrowingRecords");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualReturnDate",
                table: "BorrowingRecords",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowingRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfLateDays = table.Column<int>(type: "int", nullable: false),
                    FineAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fines_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fines_BorrowingRecords_BorrowingRecordId",
                        column: x => x.BorrowingRecordId,
                        principalTable: "BorrowingRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_AppUserId",
                table: "BorrowingRecords",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_AppUserId",
                table: "Fines",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_BorrowingRecordId",
                table: "Fines",
                column: "BorrowingRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fines_Id",
                table: "Fines",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_BorrowingRecords_AppUserId",
                table: "BorrowingRecords");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualReturnDate",
                table: "BorrowingRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_AppUserId",
                table: "BorrowingRecords",
                column: "AppUserId",
                unique: true);
        }
    }
}
