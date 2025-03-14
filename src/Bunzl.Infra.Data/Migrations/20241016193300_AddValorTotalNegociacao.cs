using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddValorTotalNegociacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "OrdemCompraAnexo");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "NegociacaoComercial",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "NegociacaoComercial");

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "OrdemCompraAnexo",
                type: "text",
                nullable: true);
        }
    }
}
