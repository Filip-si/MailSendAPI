using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class BodiesAndFootersFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_Files_FilesId",
                table: "FileAttachments");

            migrationBuilder.AddColumn<Guid>(
                name: "FileBodyId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileFooterId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FilesId",
                table: "FileAttachments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "FileBodies",
                columns: table => new
                {
                    FileBodyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFiles = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileBodies", x => x.FileBodyId);
                });

            migrationBuilder.CreateTable(
                name: "FileFooters",
                columns: table => new
                {
                    FileFooterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFiles = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileFooters", x => x.FileFooterId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileBodyId",
                table: "Files",
                column: "FileBodyId",
                unique: true,
                filter: "[FileBodyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileFooterId",
                table: "Files",
                column: "FileFooterId",
                unique: true,
                filter: "[FileFooterId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_Files_FilesId",
                table: "FileAttachments",
                column: "FilesId",
                principalTable: "Files",
                principalColumn: "FilesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileBodies_FileBodyId",
                table: "Files",
                column: "FileBodyId",
                principalTable: "FileBodies",
                principalColumn: "FileBodyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileFooters_FileFooterId",
                table: "Files",
                column: "FileFooterId",
                principalTable: "FileFooters",
                principalColumn: "FileFooterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_Files_FilesId",
                table: "FileAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileBodies_FileBodyId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileFooters_FileFooterId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "FileBodies");

            migrationBuilder.DropTable(
                name: "FileFooters");

            migrationBuilder.DropIndex(
                name: "IX_Files_FileBodyId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_FileFooterId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileBodyId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileFooterId",
                table: "Files");

            migrationBuilder.AlterColumn<Guid>(
                name: "FilesId",
                table: "FileAttachments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_Files_FilesId",
                table: "FileAttachments",
                column: "FilesId",
                principalTable: "Files",
                principalColumn: "FilesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
