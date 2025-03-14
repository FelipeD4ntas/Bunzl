using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTipoCampoTempoEntrega_e_RenomeadoCampoCapicidadeMensalFabrica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CapacidadeMensalFabricacao",
                table: "FornecedorProduto",
                newName: "CapacidadeMensalFabrica");

            migrationBuilder.AlterColumn<int>(
                name: "TempoEntrega",
                table: "FornecedorProduto",
                type: "integer",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CapacidadeMensalFabrica",
                table: "FornecedorProduto",
                newName: "CapacidadeMensalFabricacao");

            migrationBuilder.AlterColumn<decimal>(
                name: "TempoEntrega",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
