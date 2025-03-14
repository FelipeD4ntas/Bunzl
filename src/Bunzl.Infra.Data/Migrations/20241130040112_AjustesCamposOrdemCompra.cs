using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesCamposOrdemCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroEnderecoFornecedor",
                table: "OrdemCompra",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdemCompra_FornecedorId",
                table: "OrdemCompra",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemCompra_Fornecedor_FornecedorId",
                table: "OrdemCompra",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemCompra_Fornecedor_FornecedorId",
                table: "OrdemCompra");

            migrationBuilder.DropIndex(
                name: "IX_OrdemCompra_FornecedorId",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroEnderecoFornecedor",
                table: "OrdemCompra");
        }
    }
}
