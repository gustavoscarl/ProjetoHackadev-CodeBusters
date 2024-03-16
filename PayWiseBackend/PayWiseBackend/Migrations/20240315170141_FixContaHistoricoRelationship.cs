using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixContaHistoricoRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_Historicos_HistoricoId",
                table: "Contas");

            migrationBuilder.AlterColumn<int>(
                name: "HistoricoId",
                table: "Contas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_Historicos_HistoricoId",
                table: "Contas",
                column: "HistoricoId",
                principalTable: "Historicos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_Historicos_HistoricoId",
                table: "Contas");

            migrationBuilder.AlterColumn<int>(
                name: "HistoricoId",
                table: "Contas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
