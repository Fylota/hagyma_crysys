using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class addPurchased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DbUserInfoId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ff8f5270-792a-4e57-b67c-1b6364fe3ac6", "4a8ea71d-d210-4bbd-8e63-65d13b3d801b" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 11, 14, 15, 7, 9, 133, DateTimeKind.Local).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 14, 15, 7, 9, 133, DateTimeKind.Local).AddTicks(3028));

            migrationBuilder.CreateIndex(
                name: "IX_Images_DbUserInfoId",
                table: "Images",
                column: "DbUserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_DbUserInfoId",
                table: "Images",
                column: "DbUserInfoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_DbUserInfoId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_DbUserInfoId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DbUserInfoId",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "91ee5667-8cf6-45b7-acd3-c0296b56a367", "b2e5d8b2-aecb-4d49-9f92-c28f7749a861" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 11, 14, 14, 58, 49, 495, DateTimeKind.Local).AddTicks(617));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 14, 14, 58, 49, 494, DateTimeKind.Local).AddTicks(6392));
        }
    }
}
