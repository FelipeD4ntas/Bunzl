using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaNovosCamposTabelaNegociacaoComercialProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorSugerido",
                table: "NegociacaoComercialProduto",
                newName: "ValorUnitarioOriginal");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "NegociacaoComercialProduto",
                newName: "ValorUnitarioNegociado");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitarioAlvo",
                table: "NegociacaoComercialProduto",
                type: "numeric(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitarioFinal",
                table: "NegociacaoComercialProduto",
                type: "numeric(18,6)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorUnitarioAlvo",
                table: "NegociacaoComercialProduto");

            migrationBuilder.DropColumn(
                name: "ValorUnitarioFinal",
                table: "NegociacaoComercialProduto");

            migrationBuilder.RenameColumn(
                name: "ValorUnitarioOriginal",
                table: "NegociacaoComercialProduto",
                newName: "ValorSugerido");

            migrationBuilder.RenameColumn(
                name: "ValorUnitarioNegociado",
                table: "NegociacaoComercialProduto",
                newName: "Valor");
        }
    }
}
