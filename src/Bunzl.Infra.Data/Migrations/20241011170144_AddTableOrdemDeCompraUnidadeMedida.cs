using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableOrdemDeCompraUnidadeMedida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdemCompraUnidadeMedida",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdemDeCompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnidadeMedida = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    QuantidadeTotal = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemCompraUnidadeMedida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdemCompraUnidadeMedida_OrdemCompra_OrdemDeCompraId",
                        column: x => x.OrdemDeCompraId,
                        principalTable: "OrdemCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdemCompraUnidadeMedida_OrdemDeCompraId",
                table: "OrdemCompraUnidadeMedida",
                column: "OrdemDeCompraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdemCompraUnidadeMedida");
        }
    }
}
