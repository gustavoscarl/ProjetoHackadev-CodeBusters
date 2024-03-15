using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesTemContaTeCartaoToCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "temCartao",
                table: "Clientes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "temConta",
                table: "Clientes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "temCartao",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "temConta",
                table: "Clientes");
        }
    }
}
