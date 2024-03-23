using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations.SqliteMigrations
{
    /// <inheritdoc />
    public partial class AlterInvestimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investimento_Contas_ContaId",
                table: "Investimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Investimento",
                table: "Investimento");

            migrationBuilder.RenameTable(
                name: "Investimento",
                newName: "Investimentos");

            migrationBuilder.RenameIndex(
                name: "IX_Investimento_ContaId",
                table: "Investimentos",
                newName: "IX_Investimentos_ContaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Investimentos",
                table: "Investimentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Investimentos_Contas_ContaId",
                table: "Investimentos",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investimentos_Contas_ContaId",
                table: "Investimentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Investimentos",
                table: "Investimentos");

            migrationBuilder.RenameTable(
                name: "Investimentos",
                newName: "Investimento");

            migrationBuilder.RenameIndex(
                name: "IX_Investimentos_ContaId",
                table: "Investimento",
                newName: "IX_Investimento_ContaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Investimento",
                table: "Investimento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Investimento_Contas_ContaId",
                table: "Investimento",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
