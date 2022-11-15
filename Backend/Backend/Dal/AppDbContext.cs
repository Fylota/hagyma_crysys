﻿using Backend.Dal.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO.Compression;
using System.IO;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace Backend.Dal;

public class AppDbContext : ApiAuthorizationDbContext<DbUserInfo>
{
    public AppDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(
        options, operationalStoreOptions)
    {
    }

    public DbSet<DbImage> Images { get; set; }
    public DbSet<DbComment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<DbUserInfo>().HasMany(u => u.PurchasedImages);
        builder.Entity<DbImage>().Property(i => i.IsDeleted).HasDefaultValue(false);
        builder.Entity<DbImage>().HasQueryFilter(p => !p.IsDeleted);
        SeedData(builder);

        base.OnModelCreating(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
        var dummyUser = new DbUserInfo
        {
            UserName = "dummy",
            Email = "dummy@dummy.com",
            Id = "ID"
        };
        builder.Entity<DbUserInfo>().HasData(dummyUser);

        var directory = Directory.GetCurrentDirectory();

        var caffFile = Directory.GetFiles(directory,"*.caff").FirstOrDefault()!;
        var previewFile = Directory.GetFiles(directory, "*.jpg").FirstOrDefault()!;


        FileInfo caff = new FileInfo(caffFile);
        var image = new DbImage()
        {
            Id = "IMAGEID",
            Description = "Descr",
            Title = "Title",
            UploadTime = DateTime.Now,
            OwnerId = dummyUser.Id,
            CaffFileName = caff.Name,
        };
        using (var memStream = new MemoryStream())
        {
            using (var dstream = new DeflateStream(memStream, CompressionLevel.Optimal))
            {
                var bytes = File.ReadAllBytes(caffFile);
                dstream.WriteAsync(bytes, 0, bytes.Length).Wait();
            }

            image.CaffFile = memStream.ToArray();
        }

        image.Preview = Convert.ToBase64String(File.ReadAllBytes(previewFile));

        builder.Entity<DbImage>().HasData(image);

        var comment = new DbComment()
        {
            Id = "CommentID",
            UserId = dummyUser.Id,
            DbImageId = image.Id,
            CreatedDate = DateTime.Now,
            Text = "Test comment"
        };

        builder.Entity<DbComment>().HasData(comment);


    }
}