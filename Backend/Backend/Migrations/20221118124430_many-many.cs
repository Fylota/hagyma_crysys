using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class manymany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "DbImageDbUserInfo",
                columns: table => new
                {
                    BuyersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PurchasedImagesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbImageDbUserInfo", x => new { x.BuyersId, x.PurchasedImagesId });
                    table.ForeignKey(
                        name: "FK_DbImageDbUserInfo_AspNetUsers_BuyersId",
                        column: x => x.BuyersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbImageDbUserInfo_Images_PurchasedImagesId",
                        column: x => x.PurchasedImagesId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DbImageDbUserInfo_PurchasedImagesId",
                table: "DbImageDbUserInfo",
                column: "PurchasedImagesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbImageDbUserInfo");

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
                values: new object[] { "4a006ce6-bc0b-4a6a-9452-207b94478c50", "8059fac0-a2ec-4c9e-8dc6-44db8491d3ea" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: "CommentID",
                column: "CreatedDate",
                value: new DateTime(2022, 11, 18, 13, 38, 7, 541, DateTimeKind.Local).AddTicks(6809));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "IMAGEID",
                column: "UploadTime",
                value: new DateTime(2022, 11, 18, 13, 38, 7, 541, DateTimeKind.Local).AddTicks(3338));

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
    }
}
