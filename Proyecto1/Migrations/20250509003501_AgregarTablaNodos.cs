using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaNodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nodos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehiculosEnEspera = table.Column<int>(type: "int", nullable: false),
                    EstadoSemaforo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TiempoPromedioCruce = table.Column<int>(type: "int", nullable: false),
                    TipoViaNorte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoViaSur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoViaEste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoViaOeste = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nodos");
        }
    }
}
