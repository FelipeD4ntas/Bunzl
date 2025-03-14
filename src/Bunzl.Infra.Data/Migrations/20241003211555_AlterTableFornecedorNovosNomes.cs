using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableFornecedorNovosNomes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WhatsAppArea",
                table: "Fornecedor",
                newName: "WhatsAppWechatArea");

            migrationBuilder.RenameColumn(
                name: "WhatsApp",
                table: "Fornecedor",
                newName: "WhatsAppWechat");

            migrationBuilder.RenameColumn(
                name: "CodigoIdentificador",
                table: "Fornecedor",
                newName: "NumeroIdentificacaoFiscal");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "Fornecedor",
                newName: "ZipCode");

			migrationBuilder.Sql("UPDATE \"Usuario\" SET \"PerfilPermissao\" = 'BunzlCorporativoMasterUser' WHERE \"PerfilPermissao\" = 'BunzlCorporativo';");
			migrationBuilder.Sql("UPDATE \"Usuario\" SET \"PerfilPermissao\" = 'AdministradorSuperUser' WHERE \"PerfilPermissao\" = 'Administrador';");
			migrationBuilder.Sql("UPDATE \"Usuario\" SET \"PerfilPermissao\" = 'CompradorKeyUser' WHERE \"PerfilPermissao\" = 'Bunzl';");
			migrationBuilder.Sql("UPDATE \"Usuario\" SET \"PerfilPermissao\" = 'FornecedorEndUser' WHERE \"PerfilPermissao\" = 'Fornecedor';");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Fornecedor",
                newName: "Cep");

            migrationBuilder.RenameColumn(
                name: "WhatsAppWechatArea",
                table: "Fornecedor",
                newName: "WhatsAppArea");

            migrationBuilder.RenameColumn(
                name: "WhatsAppWechat",
                table: "Fornecedor",
                newName: "WhatsApp");

            migrationBuilder.RenameColumn(
                name: "NumeroIdentificacaoFiscal",
                table: "Fornecedor",
                newName: "CodigoIdentificador");

			migrationBuilder.Sql("UPDATE \"Usuario\" SET \"PerfilPermissao\" = 'BunzlCorporativo' WHERE \"PerfilPermissao\" = 'BunzlCorporativoMasterUser';");
			migrationBuilder.Sql("UPDATE \"Usuario\" SET \"PerfilPermissao\" = 'Administrador' WHERE \"PerfilPermissao\" = 'AdministradorSuperUser';");
			migrationBuilder.Sql("UPDATE \"Usuario\" SET \"PerfilPermissao\" = 'Bunzl' WHERE \"PerfilPermissao\" = 'CompradorKeyUser';");
			migrationBuilder.Sql("UPDATE \"Usuario\" SET \"PerfilPermissao\" = 'Fornecedor' WHERE \"PerfilPermissao\" = 'FornecedorEndUser';");
		}
	}
}
