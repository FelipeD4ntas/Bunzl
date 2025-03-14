using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableFornecedorNovosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlagFazemOEM",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "FlagPossuemLaboratorioProprio",
                table: "Fornecedor");

            migrationBuilder.RenameColumn(
                name: "AssinarutaComprador",
                table: "OrdemCompra",
                newName: "PrazoPagamento");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaComprador",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CnpjImportador",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoEstabelecimento",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExp",
                table: "OrdemCompra",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Despachante",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaisImportador",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CapacidadeProducaoFabricas",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpresaOfereceExclusividadeProdutosRegioes",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpresaPossuiCertificacaoFabricas",
                table: "Fornecedor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmpresaPossuiClientesNoBrasilQuemSao",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FazemOEM",
                table: "Fornecedor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PorcentagemVendasMercadosRepresentam",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PossuemLaboratorioProprio",
                table: "Fornecedor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrincipaisClientesSegmentosAtendidosEmpresa",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuaisMercadosEmpresaOpera",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuaisTermosPagamentosOferecidos",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuantidadeAnosEmpresaMercado",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuantidadeTrabalhadoresEmpresaPossui",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuantidadeTurnosTrabalhoRealizados",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuantidadeUnidadesFabricacaoOndeEstaoLocalizadas",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceitaAnualEmpresa",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssinaturaComprador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "CnpjImportador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "CodigoEstabelecimento",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "DataExp",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "Despachante",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "PaisImportador",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "CapacidadeProducaoFabricas",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EmpresaOfereceExclusividadeProdutosRegioes",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EmpresaPossuiCertificacaoFabricas",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EmpresaPossuiClientesNoBrasilQuemSao",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EmpresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "FazemOEM",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "PorcentagemVendasMercadosRepresentam",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "PossuemLaboratorioProprio",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "PrincipaisClientesSegmentosAtendidosEmpresa",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "QuaisMercadosEmpresaOpera",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "QuaisTermosPagamentosOferecidos",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "QuantidadeAnosEmpresaMercado",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "QuantidadeTrabalhadoresEmpresaPossui",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "QuantidadeTurnosTrabalhoRealizados",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "QuantidadeUnidadesFabricacaoOndeEstaoLocalizadas",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "ReceitaAnualEmpresa",
                table: "Fornecedor");

            migrationBuilder.RenameColumn(
                name: "PrazoPagamento",
                table: "OrdemCompra",
                newName: "AssinarutaComprador");

            migrationBuilder.AlterColumn<decimal>(
                name: "Numero",
                table: "OrdemCompra",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

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
        }
    }
}
