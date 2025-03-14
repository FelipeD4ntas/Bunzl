using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableFornecedorProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TermoPagamento",
                table: "FornecedorProduto",
                newName: "TempoPagamento");

            migrationBuilder.RenameColumn(
                name: "PontoEmbarque",
                table: "FornecedorProduto",
                newName: "PortoEmbarque");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TempoPagamento",
                table: "FornecedorProduto",
                newName: "TermoPagamento");

            migrationBuilder.RenameColumn(
                name: "PortoEmbarque",
                table: "FornecedorProduto",
                newName: "PontoEmbarque");
        }
    }
}
