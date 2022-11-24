using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class smallpreviewRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SmallPreview",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1e4367b8-4571-4533-99ba-069ab548615a", "886283a1-3fd2-4e6d-ad03-b18e9175ede1" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 11, 24, 14, 52, 18, 71, DateTimeKind.Local).AddTicks(3079));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 24, 14, 52, 18, 70, DateTimeKind.Local).AddTicks(9663));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SmallPreview",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e985d3a9-db61-4e77-b8fa-422df51b81d7", "77307413-6769-42ca-b5ad-6b34f7625424" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 11, 24, 14, 41, 13, 194, DateTimeKind.Local).AddTicks(1153));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 24, 14, 41, 13, 193, DateTimeKind.Local).AddTicks(7731));
        }
    }
}
