using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoEntidadeFornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RazaoSocial = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    NomeFantasia = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Logradouro = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Cep = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    Pais = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EstadoProvincia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Contato = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TelefoneArea = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    WhatsAppArea = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    WhatsApp = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    FlagJaEhParceiroBunzl = table.Column<bool>(type: "boolean", nullable: false),
                    FlagFabricaAuditadaBunzl = table.Column<bool>(type: "boolean", nullable: false),
                    FabricasAuditadas = table.Column<string>(type: "text", nullable: true),
                    InformacoesProdutos = table.Column<string>(type: "text", nullable: true),
                    InformacoesGerais = table.Column<string>(type: "text", nullable: true),
                    InformacoesFabricas = table.Column<string>(type: "text", nullable: true),
                    InformacoesMercados = table.Column<string>(type: "text", nullable: true),
                    InformacoesPoliticas = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FornecedorDadoBancario",
                columns: table => new
                {
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumeroConta = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NomeBeneficiario = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Logradouro = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Cep = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    Pais = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EstadoProvincia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Swift = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FornecedorDadoBancario", x => x.FornecedorId);
                    table.ForeignKey(
                        name: "FK_FornecedorDadoBancario_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario_x_Fornecedor",
                columns: table => new
                {
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_x_Fornecedor", x => new { x.FornecedorId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_Usuario_x_Fornecedor_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_x_Fornecedor_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_x_Fornecedor_UsuarioId",
                table: "Usuario_x_Fornecedor",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FornecedorDadoBancario");

            migrationBuilder.DropTable(
                name: "Usuario_x_Fornecedor");

            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
