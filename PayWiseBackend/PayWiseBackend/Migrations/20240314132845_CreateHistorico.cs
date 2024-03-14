using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateHistorico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HistoricoId",
                table: "Transacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Historicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historicos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_HistoricoId",
                table: "Transacoes",
                column: "HistoricoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Historicos_HistoricoId",
                table: "Transacoes",
                column: "HistoricoId",
                principalTable: "Historicos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Historicos_HistoricoId",
                table: "Transacoes");

            migrationBuilder.DropTable(
                name: "Historicos");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_HistoricoId",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "HistoricoId",
                table: "Transacoes");
        }
    }
}
