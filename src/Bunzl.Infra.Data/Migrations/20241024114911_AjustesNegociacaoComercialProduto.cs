using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesNegociacaoComercialProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecoSugerido",
                table: "NegociacaoComercialProduto",
                newName: "ValorTotal");

            migrationBuilder.AddColumn<string>(
                name: "CodigoSku",
                table: "NegociacaoComercialProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "NegociacaoComercialProduto",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "NegociacaoComercialProduto",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorSugerido",
                table: "NegociacaoComercialProduto",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_NegociacaoComercial_FornecedorId",
                table: "NegociacaoComercial",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_NegociacaoComercial_Fornecedor_FornecedorId",
                table: "NegociacaoComercial",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NegociacaoComercial_Fornecedor_FornecedorId",
                table: "NegociacaoComercial");

            migrationBuilder.DropIndex(
                name: "IX_NegociacaoComercial_FornecedorId",
                table: "NegociacaoComercial");

            migrationBuilder.DropColumn(
                name: "CodigoSku",
                table: "NegociacaoComercialProduto");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "NegociacaoComercialProduto");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "NegociacaoComercialProduto");

            migrationBuilder.DropColumn(
                name: "ValorSugerido",
                table: "NegociacaoComercialProduto");

            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "NegociacaoComercialProduto",
                newName: "PrecoSugerido");
        }
    }
}
