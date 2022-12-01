using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class commentUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 1, 0, 56, 29, 492, DateTimeKind.Local).AddTicks(3931),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 30, 15, 29, 56, 419, DateTimeKind.Local).AddTicks(5120));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "717a2bb4-1dec-4dbd-8e2d-a460768bd382", "ab4bb449-8047-4acc-a156-36ad72a3203c" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 12, 1, 0, 56, 29, 493, DateTimeKind.Local).AddTicks(365));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 12, 1, 0, 56, 29, 492, DateTimeKind.Local).AddTicks(6575));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 30, 15, 29, 56, 419, DateTimeKind.Local).AddTicks(5120),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 1, 0, 56, 29, 492, DateTimeKind.Local).AddTicks(3931));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f8de8599-18d1-4b75-b63c-d0bfbd28d0b3", "45364f07-b843-4447-bdac-b799abb09c7f" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 11, 30, 15, 29, 56, 420, DateTimeKind.Local).AddTicks(641));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 30, 15, 29, 56, 419, DateTimeKind.Local).AddTicks(7322));
        }
    }
}
