using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Helpdesk.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    IsApplicationAdmin = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseIssue",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: true),
                    RequesterId = table.Column<long>(type: "bigint", nullable: true),
                    AssigneeId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseIssue_BaseIssue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "BaseIssue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseIssue_Persons_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseIssue_Persons_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_BaseIssue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "BaseIssue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessage_BaseIssue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "BaseIssue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessage_Persons_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "CompanyId", "CompanyName", "DateOfBirth", "Email", "FirstName", "IdentificationNumber", "IsApplicationAdmin", "LastName", "PersonType" },
                values: new object[,]
                {
                    { 1L, null, "Testovací Firma s.r.o.", null, "firma@example.com", null, 12345678, false, null, 1 },
                    { 2L, null, null, new DateOnly(1985, 5, 20), "jan.novak@example.com", "Jan", null, true, "Novák", 0 },
                    { 3L, null, null, new DateOnly(1990, 8, 15), "petr.svoboda@example.com", "Petr", null, false, "Svoboda", 0 }
                });

            migrationBuilder.InsertData(
                table: "BaseIssue",
                columns: new[] { "Id", "AssigneeId", "CreatedDate", "Description", "Discriminator", "DueDate", "IssueId", "Priority", "RequesterId", "Status", "Title" },
                values: new object[,]
                {
                    { 1L, 3L, new DateOnly(2025, 7, 1), "Uživatel hlásí, že se nemůže přihlásit do systému.", "Issue", new DateOnly(2025, 7, 7), null, 2, 2L, 0, "Problém s přihlášením" },
                    { 2L, 3L, new DateOnly(2025, 7, 2), "Firma nahlásila špatně spočítanou fakturu.", "Issue", new DateOnly(2025, 7, 20), null, 1, 1L, 1, "Chyba ve fakturaci" },
                    { 3L, 1L, new DateOnly(2025, 7, 1), "Zaměstnanec požaduje nové pracovní zařízení.", "Issue", new DateOnly(2025, 7, 20), null, 0, 3L, 0, "Požadavek na nové zařízení" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseIssue_AssigneeId",
                table: "BaseIssue",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseIssue_IssueId",
                table: "BaseIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseIssue_RequesterId",
                table: "BaseIssue",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_IssueId",
                table: "Document",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_IssueId",
                table: "ChatMessage",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_SenderId",
                table: "ChatMessage",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "BaseIssue");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
