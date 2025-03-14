using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableFornecedorAndFornecedorEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoERP",
                table: "Fornecedor_x_Empresa");

            migrationBuilder.AddColumn<string>(
                name: "CodigoERP",
                table: "Fornecedor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoERP",
                table: "Fornecedor");

            migrationBuilder.AddColumn<string>(
                name: "CodigoERP",
                table: "Fornecedor_x_Empresa",
                type: "text",
                nullable: true);
        }
    }
}
