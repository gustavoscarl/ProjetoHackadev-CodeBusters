using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class OnModelCreatingRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Contas_ContaId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Documentos_DocumentoId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Enderecos_EnderecoId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Sessoes_SessaoId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TentativasLogin_TentativaLoginId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_ContaId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_DocumentoId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_EnderecoId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_SessaoId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_TentativaLoginId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ContaId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "SessaoId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TentativaLoginId",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "TentativasLogin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Sessoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Enderecos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Documentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Contas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TentativasLogin_ClienteId",
                table: "TentativasLogin",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_ClienteId",
                table: "Sessoes",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ClienteId",
                table: "Documentos",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contas_ClienteId",
                table: "Contas",
                column: "ClienteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_Clientes_ClienteId",
                table: "Contas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Clientes_ClienteId",
                table: "Documentos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Clientes_ClienteId",
                table: "Enderecos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Clientes_ClienteId",
                table: "Sessoes",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TentativasLogin_Clientes_ClienteId",
                table: "TentativasLogin",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_Clientes_ClienteId",
                table: "Contas");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Clientes_ClienteId",
                table: "Documentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Clientes_ClienteId",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Clientes_ClienteId",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_TentativasLogin_Clientes_ClienteId",
                table: "TentativasLogin");

            migrationBuilder.DropIndex(
                name: "IX_TentativasLogin_ClienteId",
                table: "TentativasLogin");

            migrationBuilder.DropIndex(
                name: "IX_Sessoes_ClienteId",
                table: "Sessoes");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_ClienteId",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Contas_ClienteId",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "TentativasLogin");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Sessoes");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Contas");

            migrationBuilder.AddColumn<int>(
                name: "ContaId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentoId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessaoId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TentativaLoginId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ContaId",
                table: "Clientes",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_DocumentoId",
                table: "Clientes",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EnderecoId",
                table: "Clientes",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_SessaoId",
                table: "Clientes",
                column: "SessaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TentativaLoginId",
                table: "Clientes",
                column: "TentativaLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Contas_ContaId",
                table: "Clientes",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Documentos_DocumentoId",
                table: "Clientes",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Enderecos_EnderecoId",
                table: "Clientes",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Sessoes_SessaoId",
                table: "Clientes",
                column: "SessaoId",
                principalTable: "Sessoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TentativasLogin_TentativaLoginId",
                table: "Clientes",
                column: "TentativaLoginId",
                principalTable: "TentativasLogin",
                principalColumn: "Id");
        }
    }
}
