using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoStatusTabelaPreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"TabelaPreco\" SET \"Status\" = 'Validada' WHERE \"Status\" = 'Aprovada'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"TabelaPreco\" SET \"Status\" = 'Aprovada' WHERE \"Status\" = 'Validada'");
        }
    }
}
