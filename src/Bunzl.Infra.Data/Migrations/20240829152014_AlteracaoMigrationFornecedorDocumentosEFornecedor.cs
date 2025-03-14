using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoMigrationFornecedorDocumentosEFornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_FornecedorId",
                table: "FornecedorDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_FornecedorId",
                table: "FornecedorDocumento",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_FornecedorId",
                table: "FornecedorDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_FornecedorId",
                table: "FornecedorDocumento",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id");
        }
    }
}
