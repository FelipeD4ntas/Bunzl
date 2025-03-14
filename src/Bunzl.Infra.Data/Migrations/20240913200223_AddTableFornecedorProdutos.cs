using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableFornecedorProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FornecedorProduto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoFornecedor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DescricaoCompletaFornecedor = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DescricaoCompletaBunzl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AplicacoesPrincipais = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Composicao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Tamanho = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Cor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CodigoNCM = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    UnidadeMedidaFornecedor = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    UnidadeMedidaBunzl = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    QuantidadeMinimaPedido = table.Column<int>(type: "integer", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric(13,2)", nullable: false),
                    Incoterm = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TermoPagamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Observacoes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DetalhesEmbalagem = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PesoBrutoCaixa = table.Column<decimal>(type: "numeric(13,2)", nullable: true),
                    ComprimentoCaixa = table.Column<decimal>(type: "numeric(13,2)", nullable: false),
                    LarguraCaixa = table.Column<decimal>(type: "numeric(13,2)", nullable: false),
                    AlturaCaixa = table.Column<decimal>(type: "numeric(13,2)", nullable: false),
                    CBM = table.Column<decimal>(type: "numeric", nullable: false),
                    TempoEntrega = table.Column<decimal>(type: "numeric(13,2)", nullable: true),
                    CustoDesenvolvimentoEmbalagem = table.Column<decimal>(type: "numeric(13,2)", nullable: true),
                    CustoRotulagemEmbalagem = table.Column<decimal>(type: "numeric(13,2)", nullable: true),
                    PontoEmbarque = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    QuantidadeCarregamentoContainer20Ft = table.Column<decimal>(type: "numeric(13,2)", nullable: true),
                    QuantidadeCarregamentoContainer40Ft = table.Column<decimal>(type: "numeric(13,2)", nullable: true),
                    QuantidadeCarregamentoContainer40Hc = table.Column<decimal>(type: "numeric(13,2)", nullable: true),
                    CodigoImportador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DescricaoImportador = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CodigoArtigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DescricaoArtigo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Familia = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Grupo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SubGrupo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CodigoSku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CodigoFamiliaBunzl = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FornecedorProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FornecedorProduto_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FornecedorProduto_FornecedorId",
                table: "FornecedorProduto",
                column: "FornecedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FornecedorProduto");
        }
    }
}
