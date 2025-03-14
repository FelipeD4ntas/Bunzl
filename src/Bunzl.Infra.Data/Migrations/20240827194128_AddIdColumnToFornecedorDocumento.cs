using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdColumnToFornecedorDocumento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_FornecedorId",
                table: "FornecedorDocumento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FornecedorDocumento",
                table: "FornecedorDocumento");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "FornecedorDocumento",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FornecedorDocumento",
                table: "FornecedorDocumento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_Id",
                table: "FornecedorDocumento",
                column: "Id",
                principalTable: "Fornecedor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_Id",
                table: "FornecedorDocumento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FornecedorDocumento",
                table: "FornecedorDocumento");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FornecedorDocumento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FornecedorDocumento",
                table: "FornecedorDocumento",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FornecedorDocumento_Fornecedor_FornecedorId",
                table: "FornecedorDocumento",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id");
        }
    }
}
