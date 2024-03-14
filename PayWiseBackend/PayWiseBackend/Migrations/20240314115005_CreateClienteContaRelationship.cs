using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateClienteContaRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContaId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ContaId",
                table: "Clientes",
                column: "ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Contas_ContaId",
                table: "Clientes",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Contas_ContaId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_ContaId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ContaId",
                table: "Clientes");
        }
    }
}
