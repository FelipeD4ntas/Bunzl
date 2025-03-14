using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RafatoracaoModuloTabelaPreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"TabelaPrecoProduto\";");
            migrationBuilder.Sql("DELETE FROM \"TabelaPreco\";");

            migrationBuilder.DropColumn(
                name: "CodigoProdutoFornecedor",
                table: "TabelaPrecoProduto");

            migrationBuilder.DropColumn(
                name: "CodigoSku",
                table: "TabelaPrecoProduto");

            migrationBuilder.DropColumn(
                name: "UnidadeMedidaFornecedor",
                table: "TabelaPrecoProduto");

            migrationBuilder.AddColumn<Guid>(
                name: "ProdutoId",
                table: "TabelaPrecoProduto",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFimVigencia",
                table: "TabelaPreco",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FlagExpirada",
                table: "TabelaPreco",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPrecoProduto_ProdutoId",
                table: "TabelaPrecoProduto",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaPrecoProduto_FornecedorProduto_ProdutoId",
                table: "TabelaPrecoProduto",
                column: "ProdutoId",
                principalTable: "FornecedorProduto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaPrecoProduto_FornecedorProduto_ProdutoId",
                table: "TabelaPrecoProduto");

            migrationBuilder.DropIndex(
                name: "IX_TabelaPrecoProduto_ProdutoId",
                table: "TabelaPrecoProduto");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "TabelaPrecoProduto");

            migrationBuilder.DropColumn(
                name: "FlagExpirada",
                table: "TabelaPreco");

            migrationBuilder.AddColumn<string>(
                name: "CodigoProdutoFornecedor",
                table: "TabelaPrecoProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoSku",
                table: "TabelaPrecoProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnidadeMedidaFornecedor",
                table: "TabelaPrecoProduto",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFimVigencia",
                table: "TabelaPreco",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
