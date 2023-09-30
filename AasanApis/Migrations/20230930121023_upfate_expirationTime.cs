using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AasanApis.Migrations
{
    /// <inheritdoc />
    public partial class upfate_expirationTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ExpireTimeInSecond",
                table: "Aastan_ShahkarRequestsLog",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExpireTimeInSecond",
                table: "Aastan_ShahkarRequestsLog",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");
        }
    }
}
