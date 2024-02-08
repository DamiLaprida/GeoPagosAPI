using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoPagos.Persistence.Migrations
{
    public partial class ActualizoTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoCancha",
                table: "Jugadores");

            migrationBuilder.AddColumn<string>(
                name: "Cancha",
                table: "Torneos",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancha",
                table: "Torneos");

            migrationBuilder.AddColumn<string>(
                name: "TipoCancha",
                table: "Jugadores",
                type: "TEXT",
                nullable: true);
        }
    }
}
