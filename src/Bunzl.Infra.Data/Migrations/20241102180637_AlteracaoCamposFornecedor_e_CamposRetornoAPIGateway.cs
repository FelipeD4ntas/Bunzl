using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoCamposFornecedor_e_CamposRetornoAPIGateway : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoImportador",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "DescricaoArtigo",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "DescricaoImportador",
                table: "FornecedorProduto");

            migrationBuilder.RenameColumn(
                name: "SubGrupo",
                table: "FornecedorProduto",
                newName: "TamanhoBunzl");

            migrationBuilder.RenameColumn(
                name: "Grupo",
                table: "FornecedorProduto",
                newName: "CorBunzl");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TamanhoBunzl",
                table: "FornecedorProduto",
                newName: "SubGrupo");

            migrationBuilder.RenameColumn(
                name: "CorBunzl",
                table: "FornecedorProduto",
                newName: "Grupo");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "OrdemCompra",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoImportador",
                table: "FornecedorProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoArtigo",
                table: "FornecedorProduto",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoImportador",
                table: "FornecedorProduto",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
