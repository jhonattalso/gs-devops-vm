using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncMe.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaCorpoECapa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DS_SUMMARY",
                table: "TB_CONTENT",
                type: "NVARCHAR2(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AddColumn<string>(
                name: "DS_ARTICLE_BODY",
                table: "TB_CONTENT",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_COVER_IMAGE_URL",
                table: "TB_CONTENT",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TB_CONTENT",
                keyColumn: "ID_CONTENT",
                keyValue: 1,
                columns: new[] { "DS_ARTICLE_BODY", "DS_COVER_IMAGE_URL", "DT_PUBLISH" },
                values: new object[] { null, null, new DateTime(2025, 11, 18, 17, 24, 48, 730, DateTimeKind.Local).AddTicks(4724) });

            migrationBuilder.UpdateData(
                table: "TB_CONTENT",
                keyColumn: "ID_CONTENT",
                keyValue: 2,
                columns: new[] { "DS_ARTICLE_BODY", "DS_COVER_IMAGE_URL", "DT_PUBLISH" },
                values: new object[] { null, null, new DateTime(2025, 11, 18, 17, 24, 48, 730, DateTimeKind.Local).AddTicks(5016) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DS_ARTICLE_BODY",
                table: "TB_CONTENT");

            migrationBuilder.DropColumn(
                name: "DS_COVER_IMAGE_URL",
                table: "TB_CONTENT");

            migrationBuilder.AlterColumn<string>(
                name: "DS_SUMMARY",
                table: "TB_CONTENT",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(300)",
                oldMaxLength: 300);

            migrationBuilder.UpdateData(
                table: "TB_CONTENT",
                keyColumn: "ID_CONTENT",
                keyValue: 1,
                column: "DT_PUBLISH",
                value: new DateTime(2025, 11, 14, 18, 32, 45, 250, DateTimeKind.Local).AddTicks(7923));

            migrationBuilder.UpdateData(
                table: "TB_CONTENT",
                keyColumn: "ID_CONTENT",
                keyValue: 2,
                column: "DT_PUBLISH",
                value: new DateTime(2025, 11, 14, 18, 32, 45, 250, DateTimeKind.Local).AddTicks(8409));
        }
    }
}
