using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CamposNovosDaPO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosicaoItem",
                table: "OrdemCompraProduto",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcordoFornecimento",
                table: "OrdemCompra",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Frete",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OutrasDespesas",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxaEmbalagem",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxaInterna",
                table: "OrdemCompra",
                type: "numeric(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosicaoItem",
                table: "OrdemCompraProduto");

            migrationBuilder.DropColumn(
                name: "AcordoFornecimento",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "Frete",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "OutrasDespesas",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "TaxaEmbalagem",
                table: "OrdemCompra");

            migrationBuilder.DropColumn(
                name: "TaxaInterna",
                table: "OrdemCompra");
        }
    }
}
