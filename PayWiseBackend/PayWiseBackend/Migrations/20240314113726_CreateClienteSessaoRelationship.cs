using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateClienteSessaoRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessaoId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_SessaoId",
                table: "Clientes",
                column: "SessaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Sessoes_SessaoId",
                table: "Clientes",
                column: "SessaoId",
                principalTable: "Sessoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Sessoes_SessaoId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_SessaoId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "SessaoId",
                table: "Clientes");
        }
    }
}
