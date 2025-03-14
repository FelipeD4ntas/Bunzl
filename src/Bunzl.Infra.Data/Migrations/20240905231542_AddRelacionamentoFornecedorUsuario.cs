using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionamentoFornecedorUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_x_Empresa_Empresa_EmpresaId",
                table: "Usuario_x_Empresa");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_x_Empresa_Usuario_UsuarioId",
                table: "Usuario_x_Empresa");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_x_Fornecedor_Fornecedor_FornecedorId",
                table: "Usuario_x_Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_x_Fornecedor_Usuario_UsuarioId",
                table: "Usuario_x_Fornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_x_Empresa_EmpresaId",
                table: "Usuario_x_Empresa",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_x_Empresa_UsuarioId",
                table: "Usuario_x_Empresa",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_x_Fornecedor_FornecedorId",
                table: "Usuario_x_Fornecedor",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_x_Fornecedor_UsuarioId",
                table: "Usuario_x_Fornecedor",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_x_Empresa_EmpresaId",
                table: "Usuario_x_Empresa");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_x_Empresa_UsuarioId",
                table: "Usuario_x_Empresa");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_x_Fornecedor_FornecedorId",
                table: "Usuario_x_Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_x_Fornecedor_UsuarioId",
                table: "Usuario_x_Fornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_x_Empresa_Empresa_EmpresaId",
                table: "Usuario_x_Empresa",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_x_Empresa_Usuario_UsuarioId",
                table: "Usuario_x_Empresa",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_x_Fornecedor_Fornecedor_FornecedorId",
                table: "Usuario_x_Fornecedor",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_x_Fornecedor_Usuario_UsuarioId",
                table: "Usuario_x_Fornecedor",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
