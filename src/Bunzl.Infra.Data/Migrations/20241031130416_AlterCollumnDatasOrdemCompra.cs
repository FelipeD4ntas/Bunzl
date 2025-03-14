using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterCollumnDatasOrdemCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacidade",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Localizacao",
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
                name: "Turnos",
                table: "Fornecedor");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TempoEstimadoPardida",
                table: "OrdemCompraProduto",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVendedor",
                table: "OrdemCompra",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataExp",
                table: "OrdemCompra",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataComprador",
                table: "OrdemCompra",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TempoEstimadoPardida",
                table: "OrdemCompraProduto",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVendedor",
                table: "OrdemCompra",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataExp",
                table: "OrdemCompra",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataComprador",
                table: "OrdemCompra",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AddColumn<int>(
                name: "Capacidade",
                table: "Fornecedor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "Fornecedor",
                type: "text",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "Turnos",
                table: "Fornecedor",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
