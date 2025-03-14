using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnCodigoFornecedorOrdemCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroContainer20Ft",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroContainer40Ft",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroContainer40Hc",
                table: "OrdemCompra");

            migrationBuilder.AddColumn<string>(
                name: "ContatoFornecedor",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroContainer",
                table: "OrdemCompra",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContatoFornecedor",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "NumeroContainer",
                table: "OrdemCompra");

            migrationBuilder.AddColumn<decimal>(
                name: "NumeroContainer20Ft",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NumeroContainer40Ft",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NumeroContainer40Hc",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: true);
        }
    }
}
