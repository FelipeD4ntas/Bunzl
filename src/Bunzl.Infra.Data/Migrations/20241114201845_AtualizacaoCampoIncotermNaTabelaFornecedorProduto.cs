using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoCampoIncotermNaTabelaFornecedorProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Incoterm",
                table: "FornecedorProduto");

            migrationBuilder.AddColumn<Guid>(
                name: "IncotermId",
                table: "FornecedorProduto",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('aa36fa32-d5fd-4194-9de7-ba3573eefb01', 'EXW', 'Ex Works', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('caecf979-a197-4caf-8ad4-04d57a6b61f2', 'FCA', 'Free Carrier', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('8539fbd9-f3d9-46a3-bffb-778fa5477dc6', 'CPT', 'Carriage Paid To', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('007c6bb3-3601-4be8-8b3b-604dc18ee7c2', 'CIP', 'Carriage and Insurance Paid To', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('2992eb03-8a77-4369-9ac4-431ffd43767d', 'DAP', 'Delivered at Place', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('55904b81-7e8e-44f7-addc-1c8291a00eeb', 'DPU', 'Delivered at Place Unloaded', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('f69141cf-804b-47a5-a162-06a48d3ee9d5', 'DDP', 'Delivered Duty Paid', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('706b0b1b-7c0c-44d3-bcd2-40f03ba9f081', 'FAS', 'Free Alongside Ship', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('d6eb9e4b-0452-4d75-80ac-36d7322ee6e6', 'FOB', 'Free On Board', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('37a4c26c-9cc6-487d-a15f-be74f1e41a18', 'CFR', 'Cost and Freight', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");
            migrationBuilder.Sql("INSERT INTO \"Incoterm\" VALUES ('0dd35c48-d82a-4357-a2c7-f5d6121c7618', 'CIF', 'Cost, Insurance and Freight', '2024-11-17 13:00:00', '2024-11-17 13:00:00', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');");

            migrationBuilder.Sql("UPDATE \"FornecedorProduto\" SET \"IncotermId\" = 'd6eb9e4b-0452-4d75-80ac-36d7322ee6e6';");

            migrationBuilder.CreateIndex(
                name: "IX_FornecedorProduto_IncotermId",
                table: "FornecedorProduto",
                column: "IncotermId");

            migrationBuilder.AddForeignKey(
                name: "FK_FornecedorProduto_Incoterm_IncotermId",
                table: "FornecedorProduto",
                column: "IncotermId",
                principalTable: "Incoterm",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FornecedorProduto_Incoterm_IncotermId",
                table: "FornecedorProduto");

            migrationBuilder.DropIndex(
                name: "IX_FornecedorProduto_IncotermId",
                table: "FornecedorProduto");

            migrationBuilder.DropColumn(
                name: "IncotermId",
                table: "FornecedorProduto");

            migrationBuilder.AddColumn<string>(
                name: "Incoterm",
                table: "FornecedorProduto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
