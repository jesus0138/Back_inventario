using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventario.Migrations
{
    /// <inheritdoc />
    public partial class CambiarComportamientoBorrado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionCarros_Carros_CarroId",
                table: "AsignacionCarros");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionCarros_Personas_PersonaId",
                table: "AsignacionCarros");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionCarros_Usuarios_AsignadoPorUsuarioId",
                table: "AsignacionCarros");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionHerramientas_Cuadrillas_CuadrillaId",
                table: "AsignacionHerramientas");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionHerramientas_Herramientas_HerramientaId",
                table: "AsignacionHerramientas");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionHerramientas_Personas_PersonaId",
                table: "AsignacionHerramientas");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionCarros_Carros_CarroId",
                table: "AsignacionCarros",
                column: "CarroId",
                principalTable: "Carros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionCarros_Personas_PersonaId",
                table: "AsignacionCarros",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionCarros_Usuarios_AsignadoPorUsuarioId",
                table: "AsignacionCarros",
                column: "AsignadoPorUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionHerramientas_Cuadrillas_CuadrillaId",
                table: "AsignacionHerramientas",
                column: "CuadrillaId",
                principalTable: "Cuadrillas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionHerramientas_Herramientas_HerramientaId",
                table: "AsignacionHerramientas",
                column: "HerramientaId",
                principalTable: "Herramientas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionHerramientas_Personas_PersonaId",
                table: "AsignacionHerramientas",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionCarros_Carros_CarroId",
                table: "AsignacionCarros");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionCarros_Personas_PersonaId",
                table: "AsignacionCarros");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionCarros_Usuarios_AsignadoPorUsuarioId",
                table: "AsignacionCarros");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionHerramientas_Cuadrillas_CuadrillaId",
                table: "AsignacionHerramientas");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionHerramientas_Herramientas_HerramientaId",
                table: "AsignacionHerramientas");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionHerramientas_Personas_PersonaId",
                table: "AsignacionHerramientas");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionCarros_Carros_CarroId",
                table: "AsignacionCarros",
                column: "CarroId",
                principalTable: "Carros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionCarros_Personas_PersonaId",
                table: "AsignacionCarros",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionCarros_Usuarios_AsignadoPorUsuarioId",
                table: "AsignacionCarros",
                column: "AsignadoPorUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionHerramientas_Cuadrillas_CuadrillaId",
                table: "AsignacionHerramientas",
                column: "CuadrillaId",
                principalTable: "Cuadrillas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionHerramientas_Herramientas_HerramientaId",
                table: "AsignacionHerramientas",
                column: "HerramientaId",
                principalTable: "Herramientas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionHerramientas_Personas_PersonaId",
                table: "AsignacionHerramientas",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
