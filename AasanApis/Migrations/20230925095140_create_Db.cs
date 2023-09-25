using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AasanApis.Migrations
{
    /// <inheritdoc />
    public partial class create_Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aastan_LOG_REQ",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    LogDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    JsonReq = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PublicAppId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ServiceId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PublicReqId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aastan_LOG_REQ", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "NAJI_ACCESS_TOCKEN",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
            //        TockenDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
            //        accessTocken = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
            //        TokenName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NAJI_ACCESS_TOCKEN", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Aastan_LOG_RES",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ResCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HTTPStatusCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    JsonRes = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PublicReqId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ReqLogId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aastan_LOG_RES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aastan_LOG_RES_Aastan_LOG_REQ_ReqLogId",
                        column: x => x.ReqLogId,
                        principalTable: "Aastan_LOG_REQ",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aastan_LOG_REQ_Id",
                table: "Aastan_LOG_REQ",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aastan_LOG_RES_ReqLogId",
                table: "Aastan_LOG_RES",
                column: "ReqLogId",
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_NAJI_ACCESS_TOCKEN_Id",
            //    table: "NAJI_ACCESS_TOCKEN",
            //    column: "Id",
            //    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aastan_LOG_RES");

            migrationBuilder.DropTable(
                name: "NAJI_ACCESS_TOCKEN");

            migrationBuilder.DropTable(
                name: "Aastan_LOG_REQ");
        }
    }
}
