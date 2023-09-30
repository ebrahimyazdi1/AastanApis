using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AasanApis.Migrations
{
    /// <inheritdoc />
    public partial class adding_ExpireDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDateTime",
                table: "Aastan_ShahkarRequestsLog",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDateTime",
                table: "Aastan_ShahkarRequestsLog");
        }
    }
}
