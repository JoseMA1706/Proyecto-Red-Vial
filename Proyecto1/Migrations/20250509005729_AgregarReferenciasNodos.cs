using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class AgregarReferenciasNodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdNodoEste",
                table: "Nodos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNodoNorte",
                table: "Nodos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNodoOeste",
                table: "Nodos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNodoSur",
                table: "Nodos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNodoEste",
                table: "Nodos");

            migrationBuilder.DropColumn(
                name: "IdNodoNorte",
                table: "Nodos");

            migrationBuilder.DropColumn(
                name: "IdNodoOeste",
                table: "Nodos");

            migrationBuilder.DropColumn(
                name: "IdNodoSur",
                table: "Nodos");
        }
    }
}
