using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CapstoneTravelBlog.Migrations
{
    /// <inheritdoc />
    public partial class AddedAvatarandPhonenumbertoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93d5036b-995e-4031-bba5-97a68275b168");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d17f69f7-894f-42dd-982e-d1f8154dda63");

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ad15856-e805-4e17-8625-fdf684fd7450", "0ad15856-e805-4e17-8625-fdf684fd7450", "User", "USER" },
                    { "595ff117-ce5d-41e9-85b0-bbce66caeaec", "595ff117-ce5d-41e9-85b0-bbce66caeaec", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ad15856-e805-4e17-8625-fdf684fd7450");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "595ff117-ce5d-41e9-85b0-bbce66caeaec");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "93d5036b-995e-4031-bba5-97a68275b168", "93d5036b-995e-4031-bba5-97a68275b168", "Admin", "ADMIN" },
                    { "d17f69f7-894f-42dd-982e-d1f8154dda63", "d17f69f7-894f-42dd-982e-d1f8154dda63", "User", "USER" }
                });
        }
    }
}
