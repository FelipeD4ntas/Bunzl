using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableNegociacaoComercial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NegociacaoComercial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<long>(type: "bigint", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DataEntrega = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CampoAtuacao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TermosPagamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegociacaoComercial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NegociacaoComercialAnexo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NegociacaoComercialId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegociacaoComercialAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegociacaoComercialAnexo_NegociacaoComercial_NegociacaoCome~",
                        column: x => x.NegociacaoComercialId,
                        principalTable: "NegociacaoComercial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NegociacaoComercialProduto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    NegociacaoComercialId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(18,5)", nullable: false),
                    PrecoSugerido = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegociacaoComercialProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegociacaoComercialProduto_NegociacaoComercial_NegociacaoCo~",
                        column: x => x.NegociacaoComercialId,
                        principalTable: "NegociacaoComercial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NegociacaoComercialAnexo_NegociacaoComercialId",
                table: "NegociacaoComercialAnexo",
                column: "NegociacaoComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_NegociacaoComercialProduto_NegociacaoComercialId",
                table: "NegociacaoComercialProduto",
                column: "NegociacaoComercialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NegociacaoComercialAnexo");

            migrationBuilder.DropTable(
                name: "NegociacaoComercialProduto");

            migrationBuilder.DropTable(
                name: "NegociacaoComercial");
        }
    }
}
