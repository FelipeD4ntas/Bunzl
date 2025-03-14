using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CodigoOtpLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoOtp",
                table: "Usuario",
                type: "character varying(6)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataGeracaoCodigoOtp",
                table: "Usuario",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPrimeiroLogin",
                table: "Usuario",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoOtp",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "DataGeracaoCodigoOtp",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "DataPrimeiroLogin",
                table: "Usuario");
        }
    }
}
