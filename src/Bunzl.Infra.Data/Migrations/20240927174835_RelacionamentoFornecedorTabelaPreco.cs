using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoFornecedorTabelaPreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TabelaPreco_FornecedorId",
                table: "TabelaPreco",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaPreco_Fornecedor_FornecedorId",
                table: "TabelaPreco",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaPreco_Fornecedor_FornecedorId",
                table: "TabelaPreco");

            migrationBuilder.DropIndex(
                name: "IX_TabelaPreco_FornecedorId",
                table: "TabelaPreco");
        }
    }
}
