using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnDataVigenciaTabelaPreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoERP",
                table: "TabelaPreco",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFimVigencia",
                table: "TabelaPreco",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicioVigencia",
                table: "TabelaPreco",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoERP",
                table: "TabelaPreco");

            migrationBuilder.DropColumn(
                name: "DataFimVigencia",
                table: "TabelaPreco");

            migrationBuilder.DropColumn(
                name: "DataInicioVigencia",
                table: "TabelaPreco");
        }
    }
}
