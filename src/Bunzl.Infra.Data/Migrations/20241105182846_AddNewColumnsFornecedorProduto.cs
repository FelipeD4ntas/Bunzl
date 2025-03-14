using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsFornecedorProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CapacidadeMensalFabricacao",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustoDetalhadoCombustivel",
                table: "FornecedorProduto",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustoDetalhadoEmbalagem",
                table: "FornecedorProduto",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustoDetalhadoEnergia",
                table: "FornecedorProduto",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustoDetalhadoMaoDeObra",
                table: "FornecedorProduto",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustoDetalhadoMateriaPrima",
                table: "FornecedorProduto",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustoDetalhadoTransporte",
                table: "FornecedorProduto",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NomeFabrica",
                table: "FornecedorProduto",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadePorCaixaMaster",
                table: "FornecedorProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadePorEmbalagemInterna",
                table: "FornecedorProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoCaixaMaster",
                table: "FornecedorProduto",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoEmbalagemInterna",
                table: "FornecedorProduto",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadeMedidaCapacidadeMensal",
                table: "FornecedorProduto",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FlagBloqueadoErp",
                table: "Fornecedor",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadeMensalFabricacao",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "CustoDetalhadoCombustivel",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "CustoDetalhadoEmbalagem",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "CustoDetalhadoEnergia",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "CustoDetalhadoMaoDeObra",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "CustoDetalhadoMateriaPrima",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "CustoDetalhadoTransporte",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "NomeFabrica",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "QuantidadePorCaixaMaster",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "QuantidadePorEmbalagemInterna",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "TipoCaixaMaster",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "TipoEmbalagemInterna",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "UnidadeMedidaCapacidadeMensal",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "FlagBloqueadoErp",
                table: "Fornecedor");
        }
    }
}
