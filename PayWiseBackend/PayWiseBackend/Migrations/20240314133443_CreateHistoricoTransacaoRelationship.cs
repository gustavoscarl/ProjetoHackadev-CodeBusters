using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateHistoricoTransacaoRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Historicos_HistoricoId",
                table: "Transacoes");

            migrationBuilder.AlterColumn<int>(
                name: "HistoricoId",
                table: "Transacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Historicos_HistoricoId",
                table: "Transacoes",
                column: "HistoricoId",
                principalTable: "Historicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Historicos_HistoricoId",
                table: "Transacoes");

            migrationBuilder.AlterColumn<int>(
                name: "HistoricoId",
                table: "Transacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Historicos_HistoricoId",
                table: "Transacoes",
                column: "HistoricoId",
                principalTable: "Historicos",
                principalColumn: "Id");
        }
    }
}
