using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class RegistrationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 30, 15, 29, 56, 419, DateTimeKind.Local).AddTicks(5120));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "AspNetUsers");

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
    }
}
