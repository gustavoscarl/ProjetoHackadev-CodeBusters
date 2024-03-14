using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class CriadoClienteIdConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Contas_ContaId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_ContaId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DataModificacao",
                table: "Contas");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Contas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContaId1",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ContaId1",
                table: "Clientes",
                column: "ContaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Contas_ContaId1",
                table: "Clientes",
                column: "ContaId1",
                principalTable: "Contas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Contas_ContaId1",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_ContaId1",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "ContaId1",
                table: "Clientes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataModificacao",
                table: "Contas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
