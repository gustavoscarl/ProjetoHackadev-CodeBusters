using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixClienteTentativaLoginRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TentativasLogin_TentativaLoginId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "TentativaLoginId",
                table: "Clientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TentativasLogin_TentativaLoginId",
                table: "Clientes",
                column: "TentativaLoginId",
                principalTable: "TentativasLogin",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TentativasLogin_TentativaLoginId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "TentativaLoginId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TentativasLogin_TentativaLoginId",
                table: "Clientes",
                column: "TentativaLoginId",
                principalTable: "TentativasLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
