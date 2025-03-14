using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InclusaoTabelaMoeda_e_AlteracaoCampoMoedaNoFornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moeda",
                table: "Fornecedor");

            migrationBuilder.AddColumn<Guid>(
                name: "MoedaId",
                table: "Fornecedor",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Moeda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sigla = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCriacao = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAlteracao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moeda", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO \"Moeda\" VALUES ('aa36fa32-d5fd-4194-9de7-ba3573eefb01', 'HKD', 'Hong Kong Dollar', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Moeda\" VALUES ('caecf979-a197-4caf-8ad4-04d57a6b61f2', 'USD', 'US Dollar', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Moeda\" VALUES ('8539fbd9-f3d9-46a3-bffb-778fa5477dc6', 'EUR', 'Euro', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Moeda\" VALUES ('007c6bb3-3601-4be8-8b3b-604dc18ee7c2', 'CHF', 'Swiss Franc', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Moeda\" VALUES ('2992eb03-8a77-4369-9ac4-431ffd43767d', 'CNY', 'Renminbi Yuan', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Moeda\" VALUES ('55904b81-7e8e-44f7-addc-1c8291a00eeb', 'JPY', 'Japanese Yen', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Moeda\" VALUES ('f69141cf-804b-47a5-a162-06a48d3ee9d5', 'GBP', 'British Pound Sterling', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");

            migrationBuilder.Sql("UPDATE \"Fornecedor\" SET \"MoedaId\" = 'aa36fa32-d5fd-4194-9de7-ba3573eefb01';");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPreco_EmpresaId",
                table: "TabelaPreco",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_MoedaId",
                table: "Fornecedor",
                column: "MoedaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Moeda_MoedaId",
                table: "Fornecedor",
                column: "MoedaId",
                principalTable: "Moeda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaPreco_Empresa_EmpresaId",
                table: "TabelaPreco",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Moeda_MoedaId",
                table: "Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_TabelaPreco_Empresa_EmpresaId",
                table: "TabelaPreco");

            migrationBuilder.DropTable(
                name: "Moeda");

            migrationBuilder.DropIndex(
                name: "IX_TabelaPreco_EmpresaId",
                table: "TabelaPreco");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_MoedaId",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "MoedaId",
                table: "Fornecedor");

            migrationBuilder.AddColumn<string>(
                name: "Moeda",
                table: "Fornecedor",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
