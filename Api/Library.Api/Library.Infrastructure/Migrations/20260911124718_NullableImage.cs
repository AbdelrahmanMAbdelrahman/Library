using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NullableImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_UploadedFiles_UploadedFileId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UploadedFileId",
                table: "Books");

            migrationBuilder.AlterColumn<Guid>(
                name: "UploadedFileId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UploadedFileId",
                table: "Books",
                column: "UploadedFileId",
                unique: true,
                filter: "[UploadedFileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_UploadedFiles_UploadedFileId",
                table: "Books",
                column: "UploadedFileId",
                principalTable: "UploadedFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_UploadedFiles_UploadedFileId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UploadedFileId",
                table: "Books");

            migrationBuilder.AlterColumn<Guid>(
                name: "UploadedFileId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_UploadedFileId",
                table: "Books",
                column: "UploadedFileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_UploadedFiles_UploadedFileId",
                table: "Books",
                column: "UploadedFileId",
                principalTable: "UploadedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
