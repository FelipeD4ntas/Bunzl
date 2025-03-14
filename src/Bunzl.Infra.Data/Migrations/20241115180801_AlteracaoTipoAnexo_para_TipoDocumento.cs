using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoTipoAnexo_para_TipoDocumento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoAnexo",
                table: "FornecedorProdutoAnexo",
                newName: "TipoDocumento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoDocumento",
                table: "FornecedorProdutoAnexo",
                newName: "TipoAnexo");
        }
    }
}
