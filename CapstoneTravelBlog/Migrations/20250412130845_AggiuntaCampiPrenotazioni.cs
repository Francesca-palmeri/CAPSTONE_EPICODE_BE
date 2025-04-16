using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CapstoneTravelBlog.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaCampiPrenotazioni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02633a4e-e0d0-4d0b-9272-a64c9c882edc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71858012-560f-43f8-a9d8-603d2acd56bb");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Prenotazioni",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroPartecipanti",
                table: "Prenotazioni",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipologia",
                table: "Prenotazioni",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "93d5036b-995e-4031-bba5-97a68275b168", "93d5036b-995e-4031-bba5-97a68275b168", "Admin", "ADMIN" },
                    { "d17f69f7-894f-42dd-982e-d1f8154dda63", "d17f69f7-894f-42dd-982e-d1f8154dda63", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93d5036b-995e-4031-bba5-97a68275b168");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d17f69f7-894f-42dd-982e-d1f8154dda63");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Prenotazioni");

            migrationBuilder.DropColumn(
                name: "NumeroPartecipanti",
                table: "Prenotazioni");

            migrationBuilder.DropColumn(
                name: "Tipologia",
                table: "Prenotazioni");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02633a4e-e0d0-4d0b-9272-a64c9c882edc", "02633a4e-e0d0-4d0b-9272-a64c9c882edc", "Admin", "ADMIN" },
                    { "71858012-560f-43f8-a9d8-603d2acd56bb", "71858012-560f-43f8-a9d8-603d2acd56bb", "User", "USER" }
                });
        }
    }
}
