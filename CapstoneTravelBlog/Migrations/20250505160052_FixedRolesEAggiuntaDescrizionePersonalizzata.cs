using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneTravelBlog.Migrations
{
    /// <inheritdoc />
    public partial class FixedRolesEAggiuntaDescrizionePersonalizzata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescrizionePersonalizzata",
                table: "Prenotazioni",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescrizionePersonalizzata",
                table: "Prenotazioni");
        }
    }
}
