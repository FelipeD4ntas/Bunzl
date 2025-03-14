using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjusteUnidadeMedidaFornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnidadeMedidaFornecedor",
                table: "FornecedorProduto",
                newName: "UnidadeMedidaFornecedorPreco");

            migrationBuilder.AddColumn<string>(
                name: "UnidadeMedidaFornecedorMOQ",
                table: "FornecedorProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE \"FornecedorProduto\" SET \"UnidadeMedidaFornecedorMOQ\" = \"UnidadeMedidaFornecedorPreco\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadeMedidaFornecedorMOQ",
                table: "FornecedorProduto");

            migrationBuilder.RenameColumn(
                name: "UnidadeMedidaFornecedorPreco",
                table: "FornecedorProduto",
                newName: "UnidadeMedidaFornecedor");
        }
    }
}
