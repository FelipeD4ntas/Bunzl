using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnFamiliaBunzlProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoFamiliaBunzl",
                table: "FornecedorProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoFamiliaBunzl",
                table: "FornecedorProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
