using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateClienteDocumentoRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentoId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_DocumentoId",
                table: "Clientes",
                column: "DocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Documentos_DocumentoId",
                table: "Clientes",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Documentos_DocumentoId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_DocumentoId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "Clientes");
        }
    }
}
