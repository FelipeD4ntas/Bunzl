using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class NovosCamposFornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempoPagamento",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "TempoPagamento",
                table: "FornecedorProduto");

            migrationBuilder.RenameColumn(
                name: "InformacoesProdutos",
                table: "Fornecedor",
                newName: "QuaisTiposProdutosFabricam");

            migrationBuilder.RenameColumn(
                name: "InformacoesFabricas",
                table: "Fornecedor",
                newName: "QuaisProdutosTercerizam");

            migrationBuilder.AddColumn<string>(
                name: "TermoPagamento",
                table: "OrdemCompra",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermoPagamento",
                table: "FornecedorProduto",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacidade",
                table: "Fornecedor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "FlagFazemOEM",
                table: "Fornecedor",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FlagPossuemLaboratorioProprio",
                table: "Fornecedor",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moeda",
                table: "Fornecedor",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumeroTrabalhadores",
                table: "Fornecedor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroUnidades",
                table: "Fornecedor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Producao",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuaisProdutosMaisVendidos",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Turnos",
                table: "Fornecedor",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TermoPagamento",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "TermoPagamento",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "Capacidade",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "FlagFazemOEM",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "FlagPossuemLaboratorioProprio",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Moeda",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "NumeroTrabalhadores",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "NumeroUnidades",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Producao",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "QuaisProdutosMaisVendidos",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Turnos",
                table: "Fornecedor");

            migrationBuilder.RenameColumn(
                name: "QuaisTiposProdutosFabricam",
                table: "Fornecedor",
                newName: "InformacoesProdutos");

            migrationBuilder.RenameColumn(
                name: "QuaisProdutosTercerizam",
                table: "Fornecedor",
                newName: "InformacoesFabricas");

            migrationBuilder.AddColumn<string>(
                name: "TempoPagamento",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TempoPagamento",
                table: "FornecedorProduto",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
