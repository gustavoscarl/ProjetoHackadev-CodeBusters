using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateContaHistoricoRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HistoricoId",
                table: "Contas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contas_HistoricoId",
                table: "Contas",
                column: "HistoricoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_Historicos_HistoricoId",
                table: "Contas",
                column: "HistoricoId",
                principalTable: "Historicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_Historicos_HistoricoId",
                table: "Contas");

            migrationBuilder.DropIndex(
                name: "IX_Contas_HistoricoId",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "HistoricoId",
                table: "Contas");
        }
    }
}
