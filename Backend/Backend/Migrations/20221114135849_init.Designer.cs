﻿// <auto-generated />
using System;
using Backend.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221114135849_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Backend.Dal.Entities.DbComment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DbImageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DbImageId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = "CommentID",
                            CreatedDate = new DateTime(2022, 11, 14, 14, 58, 49, 495, DateTimeKind.Local).AddTicks(617),
                            ImageId = "IMAGEID",
                            Text = "Test comment",
                            UserId = "ID"
                        });
                });

            modelBuilder.Entity("Backend.Dal.Entities.DbImage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("CaffFile")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("CaffFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Preview")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = "IMAGEID",
                            CaffFile = new byte[] { 237, 90, 123, 80, 19, 71, 24, 255, 46, 151, 144, 135, 104, 72, 229, 229, 163, 6, 197, 86, 42, 90, 6, 31, 245, 213, 122, 248, 2, 149, 105, 17, 95, 168, 180, 90, 165, 26, 209, 10, 226, 163, 162, 85, 78, 65, 20, 65, 27, 145, 138, 173, 51, 29, 20, 74, 139, 218, 54, 242, 240, 137, 37, 34, 34, 42, 90, 129, 250, 224, 101, 67, 80, 30, 82, 148, 4, 105, 72, 32, 185, 237, 158, 117, 108, 71, 251, 71, 107, 157, 78, 103, 186, 123, 183, 51, 151, 189, 189, 189, 239, 247, 237, 111, 119, 111, 191, 95, 80, 57, 170, 1, 135, 169, 190, 83, 124, 129, 162, 40, 120, 31, 31, 128, 170, 96, 2, 8, 40, 138, 63, 31, 39, 90, 68, 243, 201, 78, 40, 164, 69, 98, 59, 177, 152, 207, 18, 105, 23, 169, 68, 34, 147, 136, 197, 178, 174, 50, 89, 23, 123, 156, 196, 210, 110, 242, 174, 246, 221, 248, 107, 190, 17, 254, 113, 254, 41, 124, 218, 75, 196, 18, 190, 198, 223, 75, 232, 44, 40, 36, 130, 92, 193, 118, 154, 234, 7, 2, 5, 69, 43, 40, 116, 30, 148, 0, 148, 8, 91, 203, 27, 252, 56, 81, 2, 90, 40, 178, 195, 38, 201, 186, 224, 10, 199, 28, 176, 249, 52, 45, 192, 198, 138, 132, 66, 124, 119, 19, 190, 15, 66, 133, 232, 165, 190, 222, 227, 236, 186, 7, 46, 20, 247, 139, 112, 28, 178, 37, 241, 160, 196, 125, 124, 102, 190, 211, 244, 82, 67, 255, 161, 139, 86, 69, 75, 101, 206, 46, 174, 61, 122, 190, 242, 234, 0, 143, 215, 6, 14, 27, 254, 198, 136, 145, 163, 70, 79, 152, 56, 201, 215, 111, 242, 148, 169, 51, 102, 206, 154, 29, 52, 103, 238, 188, 144, 15, 22, 47, 81, 45, 13, 93, 182, 122, 205, 218, 143, 214, 69, 174, 223, 16, 179, 53, 118, 219, 246, 184, 29, 241, 123, 146, 62, 221, 155, 188, 239, 179, 207, 247, 167, 166, 125, 153, 254, 213, 215, 25, 135, 14, 103, 101, 231, 28, 59, 126, 226, 228, 169, 211, 231, 10, 206, 23, 94, 40, 186, 120, 233, 114, 217, 143, 215, 111, 220, 188, 85, 94, 81, 169, 175, 189, 115, 183, 174, 190, 161, 241, 94, 147, 177, 245, 97, 219, 47, 166, 118, 179, 165, 131, 199, 69, 1, 253, 27, 172, 39, 200, 158, 194, 165, 192, 184, 4, 184, 15, 132, 98, 30, 23, 37, 88, 199, 87, 80, 8, 69, 125, 189, 237, 94, 26, 23, 40, 94, 24, 209, 189, 223, 144, 45, 18, 199, 241, 137, 7, 51, 243, 165, 238, 67, 167, 27, 156, 22, 173, 42, 149, 57, 247, 31, 166, 127, 197, 200, 67, 123, 132, 236, 175, 1, 139, 126, 46, 100, 79, 128, 253, 142, 171, 18, 236, 105, 10, 119, 30, 173, 0, 6, 108, 214, 212, 120, 15, 32, 153, 248, 128, 112, 128, 112, 128, 112, 224, 255, 192, 129, 107, 70, 85, 194, 48, 45, 21, 25, 52, 253, 214, 218, 219, 254, 1, 165, 107, 222, 118, 46, 11, 182, 77, 152, 166, 61, 233, 191, 60, 184, 75, 228, 238, 51, 217, 254, 158, 187, 19, 47, 196, 125, 115, 229, 138, 207, 52, 105, 127, 202, 167, 167, 99, 208, 53, 4, 6, 147, 87, 133, 138, 251, 36, 184, 70, 204, 245, 126, 203, 29, 193, 30, 15, 4, 219, 51, 163, 138, 153, 182, 85, 58, 96, 12, 250, 123, 74, 107, 162, 143, 210, 39, 112, 1, 131, 160, 160, 92, 187, 56, 165, 99, 84, 248, 102, 165, 121, 101, 155, 154, 155, 156, 198, 245, 41, 236, 28, 132, 160, 180, 120, 35, 224, 187, 219, 191, 100, 91, 39, 193, 38, 106, 210, 70, 132, 127, 202, 23, 217, 146, 217, 198, 186, 60, 96, 245, 5, 101, 140, 165, 159, 218, 252, 177, 75, 115, 54, 2, 191, 193, 6, 26, 65, 183, 62, 137, 8, 46, 57, 82, 109, 34, 167, 135, 8, 172, 111, 37, 152, 230, 34, 72, 75, 54, 83, 248, 237, 242, 201, 8, 238, 198, 51, 250, 230, 5, 89, 215, 108, 189, 79, 156, 149, 91, 71, 110, 240, 67, 208, 127, 154, 184, 132, 152, 76, 188, 252, 31, 32, 198, 247, 116, 205, 130, 251, 33, 134, 108, 211, 1, 238, 58, 130, 216, 80, 163, 215, 89, 165, 130, 57, 106, 213, 220, 247, 50, 251, 190, 57, 33, 244, 8, 2, 149, 37, 215, 170, 25, 211, 220, 202, 236, 211, 94, 78, 10, 107, 14, 107, 213, 245, 168, 92, 74, 151, 120, 164, 198, 195, 139, 206, 158, 13, 8, 204, 158, 46, 214, 212, 98, 83, 50, 130, 172, 89, 181, 106, 219, 176, 247, 130, 74, 30, 13, 44, 167, 4, 46, 112, 113, 167, 63, 91, 49, 208, 232, 199, 237, 127, 61, 131, 182, 68, 35, 136, 118, 69, 144, 111, 207, 234, 219, 88, 67, 14, 91, 219, 136, 224, 220, 113, 140, 98, 133, 45, 9, 193, 16, 157, 169, 200, 164, 174, 202, 171, 66, 16, 48, 155, 155, 127, 45, 19, 193, 177, 220, 43, 220, 203, 103, 108, 188, 31, 28, 188, 17, 220, 40, 101, 235, 71, 32, 104, 73, 87, 26, 66, 148, 214, 219, 46, 182, 203, 41, 157, 251, 53, 186, 38, 176, 226, 177, 27, 135, 91, 110, 106, 212, 62, 236, 133, 160, 189, 76, 109, 222, 162, 230, 190, 75, 64, 224, 173, 69, 176, 201, 179, 189, 35, 158, 212, 33, 254, 249, 71, 220, 224, 86, 106, 151, 43, 205, 67, 131, 185, 30, 172, 105, 142, 121, 159, 209, 139, 235, 57, 166, 115, 53, 231, 164, 139, 93, 27, 208, 29, 193, 18, 76, 101, 202, 20, 126, 95, 158, 57, 207, 127, 245, 221, 245, 218, 202, 128, 186, 150, 214, 227, 45, 229, 85, 235, 84, 161, 23, 60, 113, 43, 49, 184, 253, 250, 70, 237, 131, 153, 8, 170, 111, 68, 105, 217, 252, 57, 218, 58, 231, 5, 57, 39, 108, 123, 52, 142, 236, 189, 212, 38, 173, 27, 91, 115, 129, 177, 156, 246, 234, 220, 203, 180, 15, 247, 81, 106, 175, 7, 78, 2, 199, 23, 156, 255, 108, 96, 30, 105, 148, 89, 16, 112, 178, 66, 4, 30, 89, 182, 116, 109, 243, 222, 200, 52, 4, 111, 187, 204, 164, 44, 120, 181, 180, 195, 227, 238, 106, 41, 91, 125, 8, 193, 169, 195, 54, 78, 115, 86, 105, 246, 86, 115, 97, 9, 92, 93, 128, 237, 106, 80, 203, 47, 47, 196, 172, 71, 51, 157, 104, 17, 130, 79, 231, 70, 149, 233, 76, 83, 207, 4, 34, 248, 42, 225, 176, 117, 11, 113, 27, 113, 27, 97, 27, 25, 164, 100, 110, 35, 75, 2, 89, 18, 200, 74, 74, 62, 64, 200, 119, 27, 249, 220, 37, 187, 4, 178, 75, 32, 155, 43, 178, 39, 37, 91, 121, 18, 1, 33, 129, 35, 18, 56, 34, 241, 54, 18, 166, 36, 209, 221, 23, 29, 20, 111, 224, 6, 109, 28, 92, 146, 55, 210, 58, 200, 232, 21, 125, 102, 70, 136, 237, 144, 62, 197, 53, 52, 104, 222, 171, 26, 85, 195, 20, 113, 101, 243, 131, 133, 145, 170, 132, 43, 187, 146, 39, 222, 238, 149, 245, 192, 119, 118, 143, 139, 110, 53, 37, 226, 86, 28, 69, 183, 251, 9, 107, 92, 203, 56, 103, 54, 95, 201, 232, 87, 97, 93, 160, 184, 100, 172, 219, 242, 20, 67, 38, 147, 201, 100, 179, 122, 215, 204, 156, 83, 173, 99, 11, 109, 7, 235, 45, 186, 165, 167, 98, 194, 162, 110, 245, 185, 157, 155, 125, 236, 225, 78, 123, 46, 133, 61, 191, 76, 135, 5, 70, 9, 163, 196, 146, 162, 31, 22, 2, 127, 200, 219, 140, 165, 137, 119, 217, 133, 1, 29, 125, 181, 150, 245, 230, 24, 4, 73, 25, 207, 150, 208, 188, 14, 183, 43, 152, 87, 178, 15, 138, 239, 224, 104, 190, 4, 11, 139, 115, 75, 17, 40, 142, 240, 26, 164, 155, 149, 197, 38, 241, 186, 119, 241, 232, 14, 154, 235, 27, 97, 139, 101, 26, 183, 33, 168, 107, 168, 149, 115, 239, 164, 61, 93, 112, 64, 158, 130, 160, 118, 120, 56, 86, 52, 183, 42, 213, 184, 101, 119, 172, 60, 170, 252, 172, 190, 223, 242, 50, 101, 73, 140, 145, 15, 215, 11, 121, 253, 115, 208, 207, 141, 114, 115, 108, 145, 201, 17, 65, 42, 22, 78, 246, 165, 196, 49, 150, 209, 234, 103, 74, 54, 143, 197, 239, 143, 187, 193, 203, 158, 14, 81, 88, 1, 173, 81, 99, 157, 189, 238, 11, 165, 177, 231, 9, 94, 151, 31, 47, 47, 194, 61, 160, 192, 26, 105, 182, 230, 0, 83, 235, 162, 44, 159, 193, 197, 133, 115, 243, 163, 220, 176, 26, 223, 244, 76, 1, 240, 114, 133, 251, 108, 94, 23, 29, 103, 230, 77, 137, 230, 21, 215, 228, 53, 196, 121, 196, 121, 132, 121, 100, 216, 62, 231, 156, 183, 147, 89, 161, 51, 143, 209, 152, 194, 108, 233, 12, 150, 34, 19, 180, 221, 114, 188, 132, 214, 240, 156, 155, 214, 77, 53, 223, 170, 10, 54, 108, 108, 239, 24, 155, 156, 124, 180, 122, 254, 156, 171, 225, 221, 47, 95, 202, 200, 141, 43, 184, 152, 30, 159, 230, 64, 157, 75, 42, 140, 192, 211, 245, 31, 254, 146, 128, 229, 222, 167, 39, 173, 118, 4, 229, 33, 156, 52, 194, 48, 128, 147, 106, 16, 72, 253, 172, 175, 5, 208, 236, 57, 23, 124, 249, 117, 5, 115, 156, 173, 93, 155, 177, 140, 249, 176, 211, 43, 229, 46, 91, 84, 221, 218, 92, 101, 218, 145, 87, 95, 221, 210, 48, 210, 21, 58, 4, 241, 30, 7, 72, 38, 62, 32, 28, 32, 28, 32, 28, 32, 28, 32, 28, 32, 28, 16, 252, 75, 62, 144, 163, 138, 95, 1 },
                            CaffFileName = "preview.caff",
                            Description = "Descr",
                            OwnerId = "ID",
                            Preview = new byte[] { 237, 90, 123, 80, 19, 71, 24, 255, 46, 151, 144, 135, 104, 72, 229, 229, 163, 6, 197, 86, 42, 90, 6, 31, 245, 213, 122, 248, 2, 149, 105, 17, 95, 168, 180, 90, 165, 26, 209, 10, 226, 163, 162, 85, 78, 65, 20, 65, 27, 145, 138, 173, 51, 29, 20, 74, 139, 218, 54, 242, 240, 137, 37, 34, 34, 42, 90, 129, 250, 224, 101, 67, 80, 30, 82, 148, 4, 105, 72, 32, 185, 237, 158, 117, 108, 71, 251, 71, 107, 157, 78, 103, 186, 123, 183, 51, 151, 189, 189, 189, 239, 247, 237, 111, 119, 111, 191, 95, 80, 57, 170, 1, 135, 169, 190, 83, 124, 129, 162, 40, 120, 31, 31, 128, 170, 96, 2, 8, 40, 138, 63, 31, 39, 90, 68, 243, 201, 78, 40, 164, 69, 98, 59, 177, 152, 207, 18, 105, 23, 169, 68, 34, 147, 136, 197, 178, 174, 50, 89, 23, 123, 156, 196, 210, 110, 242, 174, 246, 221, 248, 107, 190, 17, 254, 113, 254, 41, 124, 218, 75, 196, 18, 190, 198, 223, 75, 232, 44, 40, 36, 130, 92, 193, 118, 154, 234, 7, 2, 5, 69, 43, 40, 116, 30, 148, 0, 148, 8, 91, 203, 27, 252, 56, 81, 2, 90, 40, 178, 195, 38, 201, 186, 224, 10, 199, 28, 176, 249, 52, 45, 192, 198, 138, 132, 66, 124, 119, 19, 190, 15, 66, 133, 232, 165, 190, 222, 227, 236, 186, 7, 46, 20, 247, 139, 112, 28, 178, 37, 241, 160, 196, 125, 124, 102, 190, 211, 244, 82, 67, 255, 161, 139, 86, 69, 75, 101, 206, 46, 174, 61, 122, 190, 242, 234, 0, 143, 215, 6, 14, 27, 254, 198, 136, 145, 163, 70, 79, 152, 56, 201, 215, 111, 242, 148, 169, 51, 102, 206, 154, 29, 52, 103, 238, 188, 144, 15, 22, 47, 81, 45, 13, 93, 182, 122, 205, 218, 143, 214, 69, 174, 223, 16, 179, 53, 118, 219, 246, 184, 29, 241, 123, 146, 62, 221, 155, 188, 239, 179, 207, 247, 167, 166, 125, 153, 254, 213, 215, 25, 135, 14, 103, 101, 231, 28, 59, 126, 226, 228, 169, 211, 231, 10, 206, 23, 94, 40, 186, 120, 233, 114, 217, 143, 215, 111, 220, 188, 85, 94, 81, 169, 175, 189, 115, 183, 174, 190, 161, 241, 94, 147, 177, 245, 97, 219, 47, 166, 118, 179, 165, 131, 199, 69, 1, 253, 27, 172, 39, 200, 158, 194, 165, 192, 184, 4, 184, 15, 132, 98, 30, 23, 37, 88, 199, 87, 80, 8, 69, 125, 189, 237, 94, 26, 23, 40, 94, 24, 209, 189, 223, 144, 45, 18, 199, 241, 137, 7, 51, 243, 165, 238, 67, 167, 27, 156, 22, 173, 42, 149, 57, 247, 31, 166, 127, 197, 200, 67, 123, 132, 236, 175, 1, 139, 126, 46, 100, 79, 128, 253, 142, 171, 18, 236, 105, 10, 119, 30, 173, 0, 6, 108, 214, 212, 120, 15, 32, 153, 248, 128, 112, 128, 112, 128, 112, 224, 255, 192, 129, 107, 70, 85, 194, 48, 45, 21, 25, 52, 253, 214, 218, 219, 254, 1, 165, 107, 222, 118, 46, 11, 182, 77, 152, 166, 61, 233, 191, 60, 184, 75, 228, 238, 51, 217, 254, 158, 187, 19, 47, 196, 125, 115, 229, 138, 207, 52, 105, 127, 202, 167, 167, 99, 208, 53, 4, 6, 147, 87, 133, 138, 251, 36, 184, 70, 204, 245, 126, 203, 29, 193, 30, 15, 4, 219, 51, 163, 138, 153, 182, 85, 58, 96, 12, 250, 123, 74, 107, 162, 143, 210, 39, 112, 1, 131, 160, 160, 92, 187, 56, 165, 99, 84, 248, 102, 165, 121, 101, 155, 154, 155, 156, 198, 245, 41, 236, 28, 132, 160, 180, 120, 35, 224, 187, 219, 191, 100, 91, 39, 193, 38, 106, 210, 70, 132, 127, 202, 23, 217, 146, 217, 198, 186, 60, 96, 245, 5, 101, 140, 165, 159, 218, 252, 177, 75, 115, 54, 2, 191, 193, 6, 26, 65, 183, 62, 137, 8, 46, 57, 82, 109, 34, 167, 135, 8, 172, 111, 37, 152, 230, 34, 72, 75, 54, 83, 248, 237, 242, 201, 8, 238, 198, 51, 250, 230, 5, 89, 215, 108, 189, 79, 156, 149, 91, 71, 110, 240, 67, 208, 127, 154, 184, 132, 152, 76, 188, 252, 31, 32, 198, 247, 116, 205, 130, 251, 33, 134, 108, 211, 1, 238, 58, 130, 216, 80, 163, 215, 89, 165, 130, 57, 106, 213, 220, 247, 50, 251, 190, 57, 33, 244, 8, 2, 149, 37, 215, 170, 25, 211, 220, 202, 236, 211, 94, 78, 10, 107, 14, 107, 213, 245, 168, 92, 74, 151, 120, 164, 198, 195, 139, 206, 158, 13, 8, 204, 158, 46, 214, 212, 98, 83, 50, 130, 172, 89, 181, 106, 219, 176, 247, 130, 74, 30, 13, 44, 167, 4, 46, 112, 113, 167, 63, 91, 49, 208, 232, 199, 237, 127, 61, 131, 182, 68, 35, 136, 118, 69, 144, 111, 207, 234, 219, 88, 67, 14, 91, 219, 136, 224, 220, 113, 140, 98, 133, 45, 9, 193, 16, 157, 169, 200, 164, 174, 202, 171, 66, 16, 48, 155, 155, 127, 45, 19, 193, 177, 220, 43, 220, 203, 103, 108, 188, 31, 28, 188, 17, 220, 40, 101, 235, 71, 32, 104, 73, 87, 26, 66, 148, 214, 219, 46, 182, 203, 41, 157, 251, 53, 186, 38, 176, 226, 177, 27, 135, 91, 110, 106, 212, 62, 236, 133, 160, 189, 76, 109, 222, 162, 230, 190, 75, 64, 224, 173, 69, 176, 201, 179, 189, 35, 158, 212, 33, 254, 249, 71, 220, 224, 86, 106, 151, 43, 205, 67, 131, 185, 30, 172, 105, 142, 121, 159, 209, 139, 235, 57, 166, 115, 53, 231, 164, 139, 93, 27, 208, 29, 193, 18, 76, 101, 202, 20, 126, 95, 158, 57, 207, 127, 245, 221, 245, 218, 202, 128, 186, 150, 214, 227, 45, 229, 85, 235, 84, 161, 23, 60, 113, 43, 49, 184, 253, 250, 70, 237, 131, 153, 8, 170, 111, 68, 105, 217, 252, 57, 218, 58, 231, 5, 57, 39, 108, 123, 52, 142, 236, 189, 212, 38, 173, 27, 91, 115, 129, 177, 156, 246, 234, 220, 203, 180, 15, 247, 81, 106, 175, 7, 78, 2, 199, 23, 156, 255, 108, 96, 30, 105, 148, 89, 16, 112, 178, 66, 4, 30, 89, 182, 116, 109, 243, 222, 200, 52, 4, 111, 187, 204, 164, 44, 120, 181, 180, 195, 227, 238, 106, 41, 91, 125, 8, 193, 169, 195, 54, 78, 115, 86, 105, 246, 86, 115, 97, 9, 92, 93, 128, 237, 106, 80, 203, 47, 47, 196, 172, 71, 51, 157, 104, 17, 130, 79, 231, 70, 149, 233, 76, 83, 207, 4, 34, 248, 42, 225, 176, 117, 11, 113, 27, 113, 27, 97, 27, 25, 164, 100, 110, 35, 75, 2, 89, 18, 200, 74, 74, 62, 64, 200, 119, 27, 249, 220, 37, 187, 4, 178, 75, 32, 155, 43, 178, 39, 37, 91, 121, 18, 1, 33, 129, 35, 18, 56, 34, 241, 54, 18, 166, 36, 209, 221, 23, 29, 20, 111, 224, 6, 109, 28, 92, 146, 55, 210, 58, 200, 232, 21, 125, 102, 70, 136, 237, 144, 62, 197, 53, 52, 104, 222, 171, 26, 85, 195, 20, 113, 101, 243, 131, 133, 145, 170, 132, 43, 187, 146, 39, 222, 238, 149, 245, 192, 119, 118, 143, 139, 110, 53, 37, 226, 86, 28, 69, 183, 251, 9, 107, 92, 203, 56, 103, 54, 95, 201, 232, 87, 97, 93, 160, 184, 100, 172, 219, 242, 20, 67, 38, 147, 201, 100, 179, 122, 215, 204, 156, 83, 173, 99, 11, 109, 7, 235, 45, 186, 165, 167, 98, 194, 162, 110, 245, 185, 157, 155, 125, 236, 225, 78, 123, 46, 133, 61, 191, 76, 135, 5, 70, 9, 163, 196, 146, 162, 31, 22, 2, 127, 200, 219, 140, 165, 137, 119, 217, 133, 1, 29, 125, 181, 150, 245, 230, 24, 4, 73, 25, 207, 150, 208, 188, 14, 183, 43, 152, 87, 178, 15, 138, 239, 224, 104, 190, 4, 11, 139, 115, 75, 17, 40, 142, 240, 26, 164, 155, 149, 197, 38, 241, 186, 119, 241, 232, 14, 154, 235, 27, 97, 139, 101, 26, 183, 33, 168, 107, 168, 149, 115, 239, 164, 61, 93, 112, 64, 158, 130, 160, 118, 120, 56, 86, 52, 183, 42, 213, 184, 101, 119, 172, 60, 170, 252, 172, 190, 223, 242, 50, 101, 73, 140, 145, 15, 215, 11, 121, 253, 115, 208, 207, 141, 114, 115, 108, 145, 201, 17, 65, 42, 22, 78, 246, 165, 196, 49, 150, 209, 234, 103, 74, 54, 143, 197, 239, 143, 187, 193, 203, 158, 14, 81, 88, 1, 173, 81, 99, 157, 189, 238, 11, 165, 177, 231, 9, 94, 151, 31, 47, 47, 194, 61, 160, 192, 26, 105, 182, 230, 0, 83, 235, 162, 44, 159, 193, 197, 133, 115, 243, 163, 220, 176, 26, 223, 244, 76, 1, 240, 114, 133, 251, 108, 94, 23, 29, 103, 230, 77, 137, 230, 21, 215, 228, 53, 196, 121, 196, 121, 132, 121, 100, 216, 62, 231, 156, 183, 147, 89, 161, 51, 143, 209, 152, 194, 108, 233, 12, 150, 34, 19, 180, 221, 114, 188, 132, 214, 240, 156, 155, 214, 77, 53, 223, 170, 10, 54, 108, 108, 239, 24, 155, 156, 124, 180, 122, 254, 156, 171, 225, 221, 47, 95, 202, 200, 141, 43, 184, 152, 30, 159, 230, 64, 157, 75, 42, 140, 192, 211, 245, 31, 254, 146, 128, 229, 222, 167, 39, 173, 118, 4, 229, 33, 156, 52, 194, 48, 128, 147, 106, 16, 72, 253, 172, 175, 5, 208, 236, 57, 23, 124, 249, 117, 5, 115, 156, 173, 93, 155, 177, 140, 249, 176, 211, 43, 229, 46, 91, 84, 221, 218, 92, 101, 218, 145, 87, 95, 221, 210, 48, 210, 21, 58, 4, 241, 30, 7, 72, 38, 62, 32, 28, 32, 28, 32, 28, 32, 28, 32, 28, 32, 28, 16, 252, 75, 62, 144, 163, 138, 95, 1 },
                            Title = "Title",
                            UploadTime = new DateTime(2022, 11, 14, 14, 58, 49, 494, DateTimeKind.Local).AddTicks(6392)
                        });
                });

            modelBuilder.Entity("Backend.Dal.Entities.DbUserInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "ID",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "91ee5667-8cf6-45b7-acd3-c0296b56a367",
                            Email = "dummy@dummy.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            Role = 0,
                            SecurityStamp = "b2e5d8b2-aecb-4d49-9f92-c28f7749a861",
                            TwoFactorEnabled = false,
                            UserName = "dummy"
                        });
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("bit");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("bit");

                    b.Property<string>("Use")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Backend.Dal.Entities.DbComment", b =>
                {
                    b.HasOne("Backend.Dal.Entities.DbImage", null)
                        .WithMany("Comments")
                        .HasForeignKey("DbImageId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Backend.Dal.Entities.DbUserInfo", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Backend.Dal.Entities.DbUserInfo", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Dal.Entities.DbUserInfo", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Backend.Dal.Entities.DbUserInfo", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Dal.Entities.DbImage", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
