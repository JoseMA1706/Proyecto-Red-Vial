using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaNodos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdNodoSur",
                table: "Nodos",
                newName: "IdSur");

            migrationBuilder.RenameColumn(
                name: "IdNodoOeste",
                table: "Nodos",
                newName: "IdOeste");

            migrationBuilder.RenameColumn(
                name: "IdNodoNorte",
                table: "Nodos",
                newName: "IdNorte");

            migrationBuilder.RenameColumn(
                name: "IdNodoEste",
                table: "Nodos",
                newName: "IdEste");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdSur",
                table: "Nodos",
                newName: "IdNodoSur");

            migrationBuilder.RenameColumn(
                name: "IdOeste",
                table: "Nodos",
                newName: "IdNodoOeste");

            migrationBuilder.RenameColumn(
                name: "IdNorte",
                table: "Nodos",
                newName: "IdNodoNorte");

            migrationBuilder.RenameColumn(
                name: "IdEste",
                table: "Nodos",
                newName: "IdNodoEste");
        }
    }
}
