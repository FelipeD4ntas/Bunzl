using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableFornecedorProdutosColumnsQuantidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "QuantidadeCarregamentoContainer40Hc",
                table: "FornecedorProduto",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuantidadeCarregamentoContainer40Ft",
                table: "FornecedorProduto",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuantidadeCarregamentoContainer20Ft",
                table: "FornecedorProduto",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeCarregamentoContainer40Hc",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeCarregamentoContainer40Ft",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeCarregamentoContainer20Ft",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
