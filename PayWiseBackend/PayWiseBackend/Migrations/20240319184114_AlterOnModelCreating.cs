using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class AlterOnModelCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historicos_Contas_ContaId",
                table: "Historicos");

            migrationBuilder.AlterColumn<int>(
                name: "ContaId",
                table: "Historicos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Historicos_Contas_ContaId",
                table: "Historicos",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historicos_Contas_ContaId",
                table: "Historicos");

            migrationBuilder.AlterColumn<int>(
                name: "ContaId",
                table: "Historicos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Historicos_Contas_ContaId",
                table: "Historicos",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id");
        }
    }
}
