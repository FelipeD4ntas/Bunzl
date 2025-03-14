using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnRevisaoOrdemCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataRevisao",
                table: "OrdemCompra",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroRevisao",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataRevisao",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroRevisao",
                table: "OrdemCompra");
        }
    }
}
