using System;
using Backend.Dal.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class smallpreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmallPreview",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

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

            
            migrationBuilder.Sql(
                $"UPDATE Images SET {nameof(DbImage.SmallPreview)} = COALESCE({nameof(DbImage.Preview)},'')"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmallPreview",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "52b0b4f3-e7d6-4fd3-a40b-a2f66ea4152f", "5ecb060c-c9ba-4ce5-aa1a-2e0143b21993" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 11, 18, 13, 44, 29, 864, DateTimeKind.Local).AddTicks(1505));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 18, 13, 44, 29, 863, DateTimeKind.Local).AddTicks(8012));
        }
    }
}
