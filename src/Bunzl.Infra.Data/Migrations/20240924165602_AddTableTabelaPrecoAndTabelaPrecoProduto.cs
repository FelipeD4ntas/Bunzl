using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTabelaPrecoAndTabelaPrecoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TempoEntrega",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeCarregamentoContainer40Hc",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeCarregamentoContainer40Ft",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeCarregamentoContainer20Ft",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PesoBruto",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Largura",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CustoRotulagemEmbalagem",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CustoDesenvolvimentoEmbalagem",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Comprimento",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Altura",
                table: "FornecedorProduto",
                type: "numeric(18,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)");

            migrationBuilder.CreateTable(
                name: "TabelaPreco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPreco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaPrecoProduto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TabelaPrecoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoSku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CodigoProdutoFornecedor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UnidadeMedidaFornecedor = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    UltimoPrecoPraticado = table.Column<decimal>(type: "numeric(18,5)", nullable: false),
                    NovoPreco = table.Column<decimal>(type: "numeric(18,5)", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPrecoProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaPrecoProduto_TabelaPreco_TabelaPrecoId",
                        column: x => x.TabelaPrecoId,
                        principalTable: "TabelaPreco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPrecoProduto_TabelaPrecoId",
                table: "TabelaPrecoProduto",
                column: "TabelaPrecoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaPrecoProduto");

            migrationBuilder.DropTable(
                name: "TabelaPreco");

            migrationBuilder.AlterColumn<decimal>(
                name: "TempoEntrega",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeCarregamentoContainer40Hc",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeCarregamentoContainer40Ft",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeCarregamentoContainer20Ft",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PesoBruto",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Largura",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CustoRotulagemEmbalagem",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CustoDesenvolvimentoEmbalagem",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Comprimento",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Altura",
                table: "FornecedorProduto",
                type: "numeric(13,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)");
        }
    }
}
