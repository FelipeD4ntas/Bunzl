using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoMigrationFornecedorObservacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FornecedorObservacao_Fornecedor_FornecedorId",
                table: "FornecedorObservacao");

            migrationBuilder.AddForeignKey(
                name: "FK_FornecedorObservacao_Fornecedor_FornecedorId",
                table: "FornecedorObservacao",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FornecedorObservacao_Fornecedor_FornecedorId",
                table: "FornecedorObservacao");

            migrationBuilder.AddForeignKey(
                name: "FK_FornecedorObservacao_Fornecedor_FornecedorId",
                table: "FornecedorObservacao",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id");
        }
    }
}
