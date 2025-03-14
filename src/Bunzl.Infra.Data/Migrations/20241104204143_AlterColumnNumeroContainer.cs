using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
	/// <inheritdoc />
	public partial class AlterColumnNumeroContainer : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			// Executa uma instrução SQL personalizada para converter o tipo de coluna com o comando USING
			migrationBuilder.Sql(@"
                ALTER TABLE ""OrdemCompra"" 
                ALTER COLUMN ""NumeroContainer"" 
                TYPE numeric(18,2) 
                USING ""NumeroContainer""::numeric(18,2);
            ");

			// Define o valor padrão e o comportamento não nulo da coluna
			migrationBuilder.Sql(@"
                UPDATE ""OrdemCompra"" 
                SET ""NumeroContainer"" = 0.0 
                WHERE ""NumeroContainer"" IS NULL;
            ");

			migrationBuilder.AlterColumn<decimal>(
				name: "NumeroContainer",
				table: "OrdemCompra",
				type: "numeric(18,2)",
				nullable: false,
				defaultValue: 0m,
				oldClrType: typeof(string),
				oldType: "text",
				oldNullable: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "NumeroContainer",
				table: "OrdemCompra",
				type: "text",
				nullable: true,
				oldClrType: typeof(decimal),
				oldType: "numeric(18,2)");
		}
	}
}
