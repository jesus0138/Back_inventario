using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventario.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEstadoDevolucionCarro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EstadoDevolucion",
                table: "AsignacionHerramientas",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoDevolucion",
                table: "AsignacionCarros",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoDevolucion",
                table: "AsignacionCarros");

            migrationBuilder.AlterColumn<string>(
                name: "EstadoDevolucion",
                table: "AsignacionHerramientas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15,
                oldNullable: true);
        }
    }
}
