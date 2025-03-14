using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableOrdemDeCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdemCompra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<decimal>(type: "numeric", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CodigoFabricante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NomeFabricante = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EnderecoFabricante = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CodigoFornecedor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NomeFornecedor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EnderecoFornecedor = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ContatoFabricante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmailFornecedor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NomeImportador = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EnderecoImportador = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ContatoImportador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmailImportador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TempoPagamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Incoterm = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ModoEntrega = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Origem = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Destino = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NumeroContainer20Ft = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    NumeroContainer40Ft = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    NumeroContainer40Hc = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    TotalCBM = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalPesoBruto = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Comprador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AssinarutaComprador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DataComprador = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Vendedor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AssinaturaVendedor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DataVendedor = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemCompra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdemCompraAnexo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    OrdemDeCompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemCompraAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdemCompraAnexo_OrdemCompra_OrdemDeCompraId",
                        column: x => x.OrdemDeCompraId,
                        principalTable: "OrdemCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdemCompraObservacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OrdemDeCompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemCompraObservacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdemCompraObservacao_OrdemCompra_OrdemDeCompraId",
                        column: x => x.OrdemDeCompraId,
                        principalTable: "OrdemCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdemCompraProduto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdemDeCompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoNCM = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CodigoSKU = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    UnidadeMedida = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Moeda = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ValorEmDolares = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TempoEstimadoPardida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumeroLote = table.Column<long>(type: "bigint", nullable: false),
                    QuantidadeTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TaxaEmbalagem = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    TaxaInterna = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Desconto = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Frete = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemCompraProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdemCompraProduto_OrdemCompra_OrdemDeCompraId",
                        column: x => x.OrdemDeCompraId,
                        principalTable: "OrdemCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdemCompraAnexo_OrdemDeCompraId",
                table: "OrdemCompraAnexo",
                column: "OrdemDeCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemCompraObservacao_OrdemDeCompraId",
                table: "OrdemCompraObservacao",
                column: "OrdemDeCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemCompraProduto_OrdemDeCompraId",
                table: "OrdemCompraProduto",
                column: "OrdemDeCompraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdemCompraAnexo");

            migrationBuilder.DropTable(
                name: "OrdemCompraObservacao");

            migrationBuilder.DropTable(
                name: "OrdemCompraProduto");

            migrationBuilder.DropTable(
                name: "OrdemCompra");
        }
    }
}
