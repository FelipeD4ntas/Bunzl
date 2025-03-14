using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefatoracaoPO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "Frete",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "PrecoUnitario",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "QuantidadeTotal",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "TaxaEmbalagem",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "TaxaInterna",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "TempoEstimadoPardida",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "ValorEmDolares",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "AssinaturaComprador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "AssinaturaVendedor",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "Comprador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "ContatoFabricante",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "DataComprador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "DataVendedor",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "Despachante",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "Incoterm",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroContainer",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "TermoPagamento",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "TotalPesoBruto",
                table: "OrdemCompra");

            migrationBuilder.RenameColumn(
                name: "Moeda",
                table: "OrdemCompraProduto",
                newName: "MoedaSigla");

            migrationBuilder.RenameColumn(
                name: "Vendedor",
                table: "OrdemCompra",
                newName: "TipoFrete");

            migrationBuilder.RenameColumn(
                name: "SeloVendedor",
                table: "OrdemCompra",
                newName: "NumeroOrdem");

            migrationBuilder.RenameColumn(
                name: "SeloComprador",
                table: "OrdemCompra",
                newName: "EstadoProvinciaImportador");

            migrationBuilder.RenameColumn(
                name: "Origem",
                table: "OrdemCompra",
                newName: "NomeVendedor");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "OrdemCompra",
                newName: "CodigoErpFornecedor");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantidade",
                table: "OrdemCompraProduto",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroLote",
                table: "OrdemCompraProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "OrdemCompraProduto",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoItem",
                table: "OrdemCompraProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataEstimadaPartida",
                table: "OrdemCompraProduto",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Etd",
                table: "OrdemCompraProduto",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrdemItem",
                table: "OrdemCompraProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "OrdemCompraProduto",
                type: "numeric(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitario",
                table: "OrdemCompraProduto",
                type: "numeric(18,6)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorTotal",
                table: "OrdemCompra",
                type: "numeric(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCBM",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PrazoPagamento",
                table: "OrdemCompra",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModoEntrega",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailImportador",
                table: "OrdemCompra",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRevisao",
                table: "OrdemCompra",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataExp",
                table: "OrdemCompra",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BairroEnderecoImportador",
                table: "OrdemCompra",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComplementoEnderecoImportador",
                table: "OrdemCompra",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataOrdem",
                table: "OrdemCompra",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeComprador",
                table: "OrdemCompra",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeDespachante",
                table: "OrdemCompra",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroContainer20",
                table: "OrdemCompra",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroContainer40",
                table: "OrdemCompra",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroContainer40HC",
                table: "OrdemCompra",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroContainerOutros",
                table: "OrdemCompra",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroEnderecoImportador",
                table: "OrdemCompra",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoTotal",
                table: "OrdemCompra",
                type: "numeric(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCodeImportador",
                table: "OrdemCompra",
                type: "character varying(9)",
                maxLength: 9,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoItem",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "DataEstimadaPartida",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "Etd",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "OrdemItem",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "ValorUnitario",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "BairroEnderecoImportador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "ComplementoEnderecoImportador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "DataOrdem",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NomeComprador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NomeDespachante",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroContainer20",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroContainer40",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroContainer40HC",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroContainerOutros",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroEnderecoImportador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "PesoTotal",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "ZipCodeImportador",
                table: "OrdemCompra");

            migrationBuilder.RenameColumn(
                name: "MoedaSigla",
                table: "OrdemCompraProduto",
                newName: "Moeda");

            migrationBuilder.RenameColumn(
                name: "TipoFrete",
                table: "OrdemCompra",
                newName: "Vendedor");

            migrationBuilder.RenameColumn(
                name: "NumeroOrdem",
                table: "OrdemCompra",
                newName: "SeloVendedor");

            migrationBuilder.RenameColumn(
                name: "NomeVendedor",
                table: "OrdemCompra",
                newName: "Origem");

            migrationBuilder.RenameColumn(
                name: "EstadoProvinciaImportador",
                table: "OrdemCompra",
                newName: "SeloComprador");

            migrationBuilder.RenameColumn(
                name: "CodigoErpFornecedor",
                table: "OrdemCompra",
                newName: "Numero");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantidade",
                table: "OrdemCompraProduto",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroLote",
                table: "OrdemCompraProduto",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "OrdemCompraProduto",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "OrdemCompraProduto",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Frete",
                table: "OrdemCompraProduto",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoUnitario",
                table: "OrdemCompraProduto",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantidadeTotal",
                table: "OrdemCompraProduto",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxaEmbalagem",
                table: "OrdemCompraProduto",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxaInterna",
                table: "OrdemCompraProduto",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TempoEstimadoPardida",
                table: "OrdemCompraProduto",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorEmDolares",
                table: "OrdemCompraProduto",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorTotal",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCBM",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrazoPagamento",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModoEntrega",
                table: "OrdemCompra",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailImportador",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRevisao",
                table: "OrdemCompra",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataExp",
                table: "OrdemCompra",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaComprador",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaVendedor",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comprador",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContatoFabricante",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "OrdemCompra",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataComprador",
                table: "OrdemCompra",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVendedor",
                table: "OrdemCompra",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Despachante",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Incoterm",
                table: "OrdemCompra",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NumeroContainer",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TermoPagamento",
                table: "OrdemCompra",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPesoBruto",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
