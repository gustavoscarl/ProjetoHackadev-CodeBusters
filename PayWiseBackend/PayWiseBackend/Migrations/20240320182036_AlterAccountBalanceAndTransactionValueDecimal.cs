using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class AlterAccountBalanceAndTransactionValueDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Transacoes",
                type: "decimal(19,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<decimal>(
                name: "Saldo",
                table: "Contas",
                type: "decimal(19,4)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "Transacoes",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Saldo",
                table: "Contas",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)");
        }
    }
}
