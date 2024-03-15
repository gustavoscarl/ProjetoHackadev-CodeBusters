using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateClienteTentativaLoginRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TentativaLoginId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TentativaLoginId",
                table: "Clientes",
                column: "TentativaLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TentativasLogin_TentativaLoginId",
                table: "Clientes",
                column: "TentativaLoginId",
                principalTable: "TentativasLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TentativasLogin_TentativaLoginId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_TentativaLoginId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TentativaLoginId",
                table: "Clientes");
        }
    }
}
