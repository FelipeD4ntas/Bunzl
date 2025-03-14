using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFornecedorForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_Id",
                table: "FornecedorDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_FornecedorDocumento_FornecedorId",
                table: "FornecedorDocumento",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_FornecedorId",
                table: "FornecedorDocumento",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_FornecedorId",
                table: "FornecedorDocumento");

            migrationBuilder.DropIndex(
                name: "IX_FornecedorDocumento_FornecedorId",
                table: "FornecedorDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_Id",
                table: "FornecedorDocumento",
                column: "Id",
                principalTable: "Fornecedor",
                principalColumn: "Id");
        }
    }
}
