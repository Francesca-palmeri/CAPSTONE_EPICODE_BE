using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CapstoneTravelBlog.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentiEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ad15856-e805-4e17-8625-fdf684fd7450");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "595ff117-ce5d-41e9-85b0-bbce66caeaec");

            migrationBuilder.CreateTable(
                name: "Commenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Testo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UtenteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlogPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commenti_AspNetUsers_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Commenti_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "150ed783-a65c-48e5-841f-804aec8fce7e", "150ed783-a65c-48e5-841f-804aec8fce7e", "Admin", "ADMIN" },
                    { "854de5cb-7457-4473-99fd-5cce61678553", "854de5cb-7457-4473-99fd-5cce61678553", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commenti_BlogPostId",
                table: "Commenti",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Commenti_UtenteId",
                table: "Commenti",
                column: "UtenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commenti");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "150ed783-a65c-48e5-841f-804aec8fce7e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "854de5cb-7457-4473-99fd-5cce61678553");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ad15856-e805-4e17-8625-fdf684fd7450", "0ad15856-e805-4e17-8625-fdf684fd7450", "User", "USER" },
                    { "595ff117-ce5d-41e9-85b0-bbce66caeaec", "595ff117-ce5d-41e9-85b0-bbce66caeaec", "Admin", "ADMIN" }
                });
        }
    }
}
