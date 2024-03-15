using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoNomeAtributos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "temConta",
                table: "Clientes",
                newName: "TemConta");

            migrationBuilder.RenameColumn(
                name: "temCartao",
                table: "Clientes",
                newName: "TemCartao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TemConta",
                table: "Clientes",
                newName: "temConta");

            migrationBuilder.RenameColumn(
                name: "TemCartao",
                table: "Clientes",
                newName: "temCartao");
        }
    }
}
