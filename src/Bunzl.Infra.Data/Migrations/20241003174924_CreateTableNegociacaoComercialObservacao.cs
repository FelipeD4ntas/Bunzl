using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableNegociacaoComercialObservacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NegociacaoComercialObservacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    NegociacaoComercialId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegociacaoComercialObservacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegociacaoComercialObservacao_NegociacaoComercial_Negociaca~",
                        column: x => x.NegociacaoComercialId,
                        principalTable: "NegociacaoComercial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NegociacaoComercialObservacao_NegociacaoComercialId",
                table: "NegociacaoComercialObservacao",
                column: "NegociacaoComercialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NegociacaoComercialObservacao");
        }
    }
}
