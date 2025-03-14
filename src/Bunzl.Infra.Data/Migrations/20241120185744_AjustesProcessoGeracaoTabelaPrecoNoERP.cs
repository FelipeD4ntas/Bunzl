using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesProcessoGeracaoTabelaPrecoNoERP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoTabelaPreco",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FlagRegravarTabelaPrecoErp",
                table: "Empresa",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoTabelaPreco",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "FlagRegravarTabelaPrecoErp",
                table: "Empresa");
        }
    }
}
