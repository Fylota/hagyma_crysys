using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class deleteField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fef7f230-811d-4582-b2fb-ceb8ea5197f1", "76052064-1cfa-468f-8c77-d4fd6aba8076" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 11, 14, 15, 36, 42, 906, DateTimeKind.Local).AddTicks(6118));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 14, 15, 36, 42, 906, DateTimeKind.Local).AddTicks(2375));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4ebaedc6-4bc0-4208-8bce-51b2d6f9c00c", "5022dba4-8158-4792-a008-1cd355fca640" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 11, 14, 15, 13, 58, 662, DateTimeKind.Local).AddTicks(3418));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 14, 15, 13, 58, 661, DateTimeKind.Local).AddTicks(9857));
        }
    }
}
