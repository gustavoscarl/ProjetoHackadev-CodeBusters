using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class AlterHistoricoContaRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ContaId",
                table: "Historicos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_ContaId",
                table: "Historicos",
                column: "ContaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Historicos_Contas_ContaId",
                table: "Historicos",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historicos_Contas_ContaId",
                table: "Historicos");

            migrationBuilder.DropIndex(
                name: "IX_Historicos_ContaId",
                table: "Historicos");

            migrationBuilder.DropColumn(
                name: "ContaId",
                table: "Historicos");

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
    }
}
