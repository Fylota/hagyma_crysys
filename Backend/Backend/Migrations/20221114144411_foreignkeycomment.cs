using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class foreignkeycomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Images_DbImageId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "DbImageId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8a9aa436-9ff4-4044-903f-8a343325d79d", "767dab7b-4eab-464d-91a0-c783325da75b" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                columns: new[] { "CreatedDate", "DbImageId" },
                values: new object[] { new DateTime(2022, 11, 14, 15, 44, 11, 34, DateTimeKind.Local).AddTicks(407), "IMAGEID" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 14, 15, 44, 11, 33, DateTimeKind.Local).AddTicks(6656));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Images_DbImageId",
                table: "Comments",
                column: "DbImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Images_DbImageId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "DbImageId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "CreatedDate", "DbImageId", "ImageId" },
                values: new object[] { new DateTime(2022, 11, 14, 15, 36, 42, 906, DateTimeKind.Local).AddTicks(6118), null, "IMAGEID" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 14, 15, 36, 42, 906, DateTimeKind.Local).AddTicks(2375));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Images_DbImageId",
                table: "Comments",
                column: "DbImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
