using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "FileHeaders",
                columns: table => new
                {
                    FileHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFiles = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileHeaders", x => x.FileHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileBodyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileFooterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FilesId);
                    table.ForeignKey(
                        name: "FK_Files_FileBodies_FileBodyId",
                        column: x => x.FileBodyId,
                        principalTable: "FileBodies",
                        principalColumn: "FileBodyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_FileFooters_FileFooterId",
                        column: x => x.FileFooterId,
                        principalTable: "FileFooters",
                        principalColumn: "FileFooterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_FileHeaders_FileHeaderId",
                        column: x => x.FileHeaderId,
                        principalTable: "FileHeaders",
                        principalColumn: "FileHeaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                columns: table => new
                {
                    FileAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFiles = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.FileAttachmentId);
                    table.ForeignKey(
                        name: "FK_FileAttachments_Files_FilesId",
                        column: x => x.FilesId,
                        principalTable: "Files",
                        principalColumn: "FilesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.TemplateId);
                    table.ForeignKey(
                        name: "FK_Templates_Files_FilesId",
                        column: x => x.FilesId,
                        principalTable: "Files",
                        principalColumn: "FilesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_FilesId",
                table: "FileAttachments",
                column: "FilesId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileHeaderId",
                table: "Files",
                column: "FileHeaderId",
                unique: true,
                filter: "[FileHeaderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_FilesId",
                table: "Templates",
                column: "FilesId",
                unique: true,
                filter: "[FilesId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAttachments");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "FileBodies");

            migrationBuilder.DropTable(
                name: "FileFooters");

            migrationBuilder.DropTable(
                name: "FileHeaders");
        }
    }
}
